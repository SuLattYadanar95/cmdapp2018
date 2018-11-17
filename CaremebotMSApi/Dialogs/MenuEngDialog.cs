using CaremebotMSApi.Helper;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CaremebotMSApi.Dialogs
{
    [Serializable]
    public class MenuEngDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();
            message.Text = "How can I help you!";
            message.Type = ActivityTypes.Message;
            message.TextFormat = TextFormatTypes.Plain;
            message.SuggestedActions = new SuggestedActions()
            {
                Actions = (new List<string> { "🤔 Start over", "🤟 ျပန္စမည္", "🤤 Keep going", "😇 Help", "🤝 အၾကံေပးမည္။" }).Select(a => new CardAction
                {
                    Title = a,
                    Type = ActionTypes.ImBack,
                    Value = a
                }).ToList()
            };
            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);

        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {


            var activity = await result as Activity;
            var typingReply = activity.CreateReply();
            typingReply.Type = ActivityTypes.Typing;
            await context.PostAsync(typingReply);
            if (activity.Text == "🤤 Keep going")
            {
                //context.Done<object>(null);
                context.Done(activity);
                return;
            }
            else if (activity.Text == "🤔 Start over" || activity.Text == "🤟 ျပန္စမည္" || activity.Text == "Get started")
            {               
                await context.Forward(new RootDialog(), ResumeAfterRootDialog, activity, CancellationToken.None);
                return;
            }
            else if (activity.Text == "😇 Help" || activity.Text == "🤝 အၾကံေပးမည္။")
            {
                var reply = activity.CreateReply("We are happy to help you anyway. Please call us by tapping one of those below for your specific query. We will be waiting for your call.");
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                reply.Attachments.Add(new HeroCard
                {
                    Title = "Caremebot",
                    Subtitle = "Don't hestiate to call us at anytime.",
                    Images = new List<CardImage> { new CardImage(ResourceHelper.help_img_url) },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.OpenUrl, "For support?", value:"tel:+95-952651110"),
                        new CardAction(ActionTypes.OpenUrl, "အကူအညီယူရန္", value:"tel:+95-404015130"),
                        new CardAction(ActionTypes.ImBack, "Get started", value:"get started"),
                    }
                }.ToAttachment());
                await context.PostAsync(reply);
            }
            context.Wait(MessageReceivedAsync);
        }
        private async Task ResumeAfterRootDialog(IDialogContext context, IAwaitable<object> result)
        {

            var message = context.MakeMessage();
            message.Text = "To learn more or start over!";
            message.Type = ActivityTypes.Message;
            message.TextFormat = TextFormatTypes.Plain;
            message.SuggestedActions = new SuggestedActions()
            {
                Actions = (new List<string> { "🤔 Start over", "🤟 ျပန္စမည္", "🤤 Keep going", "😇 Help", "🤝 အၾကံေပးမည္။" }).Select(a => new CardAction
                {
                    Title = a,
                    Type = ActionTypes.ImBack,
                    Value = a
                }).ToList()
            };
            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

    }
}