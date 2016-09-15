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

namespace MessagingToolkit.Messenger.Polling
{
    public sealed class OutgoingMessagePoller : Poller
    {
        /// <summary>
        /// Messenger
        /// </summary>
        private IMobileGateway messenger;

        /// <summary>
        /// Constructor
        /// </summary>
        public OutgoingMessagePoller(IMobileGateway messenger)
            : base()
        {
            this.messenger = messenger;
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void DoWork(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Disable and wait until finish execution
                this.timer.Enabled = false;

                if (messenger == null) return;

                if (logger.IsInfoEnabled) logger.Info("Checking outgoing message");

                /*
                GetUnsentMessagesCommand command = new GetUnsentMessagesCommand();
                Outgoing[] messages = CommandHandlerProxy.Process(command);
                List<IMessage> outgoingMessages = new List<IMessage>(messages.Count());
                foreach (Outgoing message in messages)
                {

                    OutgoingMessageType messageType = (OutgoingMessageType)StringEnum.Parse(typeof(OutgoingMessageType), message.msg_type);
                    if (messageType == OutgoingMessageType.SMS)
                    {
                        // Get the target routed gateway                    
                        Sms sms = EntityHelper.FromCommonRepresentation<Sms>(message.msg_content);
                        sms.Identifier = message.id;
                        IGateway gateway = messageGatewayService.Router.GetRoute(sms);
                        GetGatewayByIdCommand getGwCmd = new GetGatewayByIdCommand() { Id = gateway.Id };
                        Gateway gwConfig = CommandHandlerProxy.Process(getGwCmd);
                        if (gwConfig != null)
                        {
                            if (!string.IsNullOrEmpty(gwConfig.smsc_no))
                            {
                                sms.ServiceCenterNumber = gwConfig.smsc_no;
                            }
                        }
                        outgoingMessages.Add(sms);
                    }
                    else if (messageType == OutgoingMessageType.WAPPush)
                    {
                        Wappush wappush = EntityHelper.FromCommonRepresentation<Wappush>(message.msg_content);
                        wappush.Identifier = message.id;
                        IGateway gateway = messageGatewayService.Router.GetRoute(wappush);
                        GetGatewayByIdCommand getGwCmd = new GetGatewayByIdCommand() { Id = gateway.Id };
                        Gateway gwConfig = CommandHandlerProxy.Process(getGwCmd);
                        if (gwConfig != null)
                        {
                            if (!string.IsNullOrEmpty(gwConfig.smsc_no))
                            {
                                wappush.ServiceCenterNumber = gwConfig.smsc_no;
                            }
                        }
                        outgoingMessages.Add(wappush);
                    }
                    else if (messageType == OutgoingMessageType.vCard)
                    {

                    }
                    else if (messageType == OutgoingMessageType.vCalendar)
                    {

                    }

                    // Update status to "Sending"
                    message.status = StringEnum.GetStringValue(MessageStatus.Sending);
                    UpdateOutgoingMessageCommand updateMsgCmd = new UpdateOutgoingMessageCommand() { Message = message };
                    CommandHandlerProxy.Process(updateMsgCmd);
                }
                int count = messageGatewayService.SendMessages(outgoingMessages);
                if (count > 0)
                    logger.InfoFormat("Messages are queued for sending. Count of messages is [{0}]", count);
                */
            }
            catch (Exception ex)
            {
                logger.Error("Error polling messages", ex);
            }
            finally
            {
                this.timer.Enabled = true;
            }
        }
    }
}
