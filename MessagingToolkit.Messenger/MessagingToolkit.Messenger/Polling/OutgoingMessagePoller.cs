using MessagingToolkit.Core.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MessagingToolkit.Core;
using MessagingToolkit.Core.Base;
using MessagingToolkit.Core.Mobile.Message;
using MessagingToolkit.Messenger.Database;
using MessagingToolkit.Messenger.Model;
using MessagingToolkit.Messenger.Helper;
using MessagingToolkit.MMS;

namespace MessagingToolkit.Messenger.Polling
{
    public sealed class OutgoingMessagePoller : Poller
    {
        /// <summary>
        /// Messenger
        /// </summary>
        private IMobileGateway messenger;

        /// <summary>
        /// Database context
        /// </summary>
        private MessengerContext database;


        private MessengerService messengerService;

        /// <summary>
        /// Constructor
        /// </summary>
        public OutgoingMessagePoller(MessengerService messengerService, MessengerContext database)
            : base()
        {
            this.messengerService = messengerService;
            this.database = database;
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void DoWork(object sender, ElapsedEventArgs e)
        {
            bool isDataConnectionAvailable = false;
            try
            {
                // Disable and wait until finish execution
                this.timer.Enabled = false;

                messenger = messengerService.Messenger;
                if (messenger == null) return;

                if (logger.IsInfoEnabled) logger.Info("Checking unprocessed message");

                // Get unprocessed messages
                List<IncomingMessage> unProcessedMsgs = database.IncomingMesages.Where(msg => msg.Status == IncomingMessage.ProcessingStatus.NotProcessed).ToList<IncomingMessage>();
                if (unProcessedMsgs.Count > 0)
                {
                    logger.Info("Processing " + unProcessedMsgs.Count + " messages");
                    foreach (IncomingMessage msg in unProcessedMsgs)
                    {
                        MessageInformation msgInfo = EntityHelper.FromCommonRepresentation<MessageInformation>(msg.MsgContent);
                        logger.Info("Processing message from " + msgInfo.PhoneNumber);

                        // Check for matching employee

                        string employeeID = msgInfo.Content;
                        Employee employee = database.Employees.Find(employeeID);
                        Gateway gateway = database.Gateways.Find(GlobalConstants.DefaultGatewayID);
                        if (employee != null && gateway != null)
                        {
                            if (messenger.InitializeDataConnection())
                            {
                                isDataConnectionAvailable = true;

                                // Send MMS
                                Mms mms = Mms.NewInstance(employee.EmployeeName, gateway.GatewayPhoneNumber);

                                // Multipart mixed
                                mms.MultipartRelatedType = MultimediaMessageConstants.ContentTypeApplicationMultipartMixed;
                                mms.PresentationId = EntityHelper.GenerateGuid();
                                mms.TransactionId = EntityHelper.GenerateGuid();
                                mms.AddToAddress(msgInfo.PhoneNumber, MmsAddressType.PhoneNumber);

                                MultimediaMessageContent multimediaMessageContent = new MultimediaMessageContent();
                                multimediaMessageContent.SetContent(employee.EmployeePhoto, 0, employee.EmployeePhoto.Length);
                                multimediaMessageContent.ContentId = EntityHelper.GenerateGuid();
                                multimediaMessageContent.Type = employee.PhotoImageType;
                                mms.AddContent(multimediaMessageContent);
                                if (messenger.Send(mms))
                                {
                                    msg.Status = IncomingMessage.ProcessingStatus.Processed;
                                }
                                else
                                {
                                    msg.Status = IncomingMessage.ProcessingStatus.Error;
                                    msg.ErrorMsg = messenger.LastError.Message;
                                }
                                database.SaveChanges();
                            }
                            else
                            {                              
                                logger.Error("Unable to establish a data connection through the modem. Check if you modem support MMS");
                                if (messenger.LastError != null)
                                {
                                    logger.Error(messenger.LastError.ToString());
                                }
                            }
                        }
                        else
                        {
                            // Employee not found
                            msg.Status = IncomingMessage.ProcessingStatus.Error;
                            msg.ErrorMsg = "Employee not found";
                            database.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error polling messages", ex);
            }
            finally
            {
                if (isDataConnectionAvailable)
                {
                    messengerService.StartMessenger();
                }
                this.timer.Enabled = true;
            }
        }


    }
}
