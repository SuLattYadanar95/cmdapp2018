using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Microsoft.Bot.Builder.FormFlow;
using CaremebotMSApi.Helper;
using Infra.Helper;

namespace CaremebotMSApi.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {

            context.Wait(MessageReceivedAsync);

        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var msg = await result as Activity;
            if (msg.Text.isStart_words())
            {
                var reply = msg.CreateReply($"Welcome to Careme bot. I am here to help you. One more thing: Don’t use me in medical emergencies. I don’t provide medical advice, and I don’t support emergency calls");
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                reply.Attachments.Add(new HeroCard
                {
                    Title = "Bot for your health",
                    Text = "Ask me anything about your health. I will learn from you and keep improved.",
                    Images = new List<CardImage> { new CardImage(ResourceHelper.welcome_img_url) },
                    Buttons = new List<CardAction>
                        {
                            new CardAction(ActionTypes.ImBack, "Gift for you", value:"Gift for you"),
                            new CardAction(ActionTypes.OpenUrl, "Book a doctor", value:"Book a doctor"),
                            //new CardAction(ActionTypes.ImBack, "ခရီးသြားရေအာင္", value:"ခရီးသြားရေအာင္"),
                            new CardAction(ActionTypes.ImBack, "About us", value:"About us"),

                        }
                }.ToAttachment());

                await context.PostAsync(reply);

            }
            else if (msg.Text.isBookDoctor_words())
            {
                var reply = msg.CreateReply($"Let's get started with your preferred choice for booking a doctor");                
                    reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                    reply.Attachments = new List<HeroCard> {
                        new HeroCard
                        {
                            Title = "Find a doctor",
                            Text = "Tap here to find a doctor",
                            Images = new List<CardImage> { new CardImage(ResourceHelper.doctor_img_url) },
                            Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.OpenUrl, "Find a doctor", value:$"https://airlineticketbotclient.yammobots.com/domain"),

                            }
                        },
                        new HeroCard
                        {
                            Title = "Find a doctor with specialty",
                            Text = "Tap here to find a doctor with your preferred speciality",
                            Images = new List<CardImage> { new CardImage(ResourceHelper.specialty_img_url) },
                            Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.OpenUrl, "Start with specialty", value:$"https://airlineticketbotclient.yammobots.com/domain"),

                            }
                        },
                        new HeroCard
                        {
                            Title = "Find a doctor by hospital",
                            Text = "Tap here to find a doctor with your preferred hospital",
                            Images = new List<CardImage> { new CardImage(ResourceHelper.hospital_img_url) },
                            Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.OpenUrl, "Start with hospital", value:$"https://airlineticketbotclient.yammobots.com/domain"),

                            }
                        },
                        new HeroCard
                        {
                            Title = "Recommend a doctor",
                            Text = "Let us know your problem and we will suggest you.",
                            Images = new List<CardImage> { new CardImage(ResourceHelper.specialty_img_url) },
                            Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.ImBack, "Suggest me", value:"Suggest me"),

                            }
                        },

                    }.Select(a => a.ToAttachment()).ToList();
                await context.PostAsync(reply);
                
            }            
            else if (msg.Text.ToLower().Equals("about us"))
            {
                var objs = await DomainApiRequestHelper.Get(hospitalid: 1, tags: "About us");
                if (objs != null && objs.Count > 0)
                {

                    var reply = msg.CreateReply($"Welcome to . This is beta release bot for testing purpose to learn user insight better.");
                    reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                    reply.Attachments = objs.Select(a => new HeroCard
                    {
                        Title = a.Name,
                        Text = a.Description,
                        Images = new List<CardImage> { new CardImage(a.ImageUrl) },
                        Buttons = new List<CardAction>
                        {
                            new CardAction(ActionTypes.OpenUrl, a.Action, value:$"https://airlineticketbotclient.yammobots.com/domain/index?id={a.ID}&airlineid={1}"),
                        }
                    }).ToList().Select(a => a.ToAttachment()).ToList();
                    await context.PostAsync(reply);

                }
            }            
            else if (msg.Text.ToLower().Equals("suggest me"))
            {
                var hospitals = await HospitalApiRequestHelper.Get(pagesize: 0);
                var bodyparts = await BodyPartApiRequestHelper.Get(pagesize: 0);
                var form = new FormDialog<EngFormFlowDialog>(new EngFormFlowDialog(bodyparts, hospitals), EngFormFlowDialog.BuildForm, FormOptions.None, null);
                await context.Forward(form, ResumeAfterEngFFDialog, msg, CancellationToken.None);
                return;
            }
            else if (msg.Text.ToLower().Equals("gift for you"))
            {
                var reply = msg.CreateReply($"Here are useful stuffs for you. Just try it and suggest us.");
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                reply.Attachments = new List<HeroCard> {
                        new HeroCard
                        {
                            Title = "Free health tip and article",
                            Text = "We select the useful articles for you. Tap here to subscribe",
                            Images = new List<CardImage> { new CardImage(ResourceHelper.doctor_img_url) },
                            Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.ImBack, "Subscribe now", value:"Subscribe now"),

                            }
                        },
                        new HeroCard
                        {
                            Title = "Reminder",
                            Text = "I will remind you for your health and activity you set",
                            Images = new List<CardImage> { new CardImage(ResourceHelper.specialty_img_url) },
                            Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.ImBack, "Remind me", value:"Remind me"),

                            }
                        },
                        new HeroCard
                        {
                            Title = "Feedback",
                            Text = "You are welcome and your feedback is always appreciated.",
                            Images = new List<CardImage> { new CardImage(ResourceHelper.specialty_img_url) },
                            Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.OpenUrl, "Post suggestion", value:$"https://airlineticketbotclient.yammobots.com/domain"),

                            }
                        }
                    }.Select(a => a.ToAttachment()).ToList();

                await context.PostAsync(reply);
            }
            else if (msg.Text.ToLower().Equals("remind me"))
            {

                var form = new FormDialog<ReminderFormFlowDialog>(new ReminderFormFlowDialog(), ReminderFormFlowDialog.BuildForm, FormOptions.None, null);
                await context.Forward(form, ResumeAfterEngFFDialog, msg, CancellationToken.None);
                return;
            }
            else if (msg.Text.ToLower().Equals("subscribe now"))
            {

                var form = new FormDialog<SubscribeFormFlowDialog>(new SubscribeFormFlowDialog("a"), () => SubscribeFormFlowDialog.BuildForm("a"), FormOptions.None, null);
                await context.Forward(form, ResumeAfterEngFFDialog, msg, CancellationToken.None);
                return;
            }
            context.Wait(MessageReceivedAsync);
        }
        private async Task ResumeAfterEngFFDialog(IDialogContext context, IAwaitable<object> result)
        {

            var argument = await result as EngFormFlowDialog;
            var reply = context.MakeMessage();
            reply.Text = "Want more tickets or start over!";
            reply.Type = ActivityTypes.Message;
            reply.TextFormat = TextFormatTypes.Plain;
            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = (new List<string> { "🤔 Start over", "🤤 I am fine", "😇 Help", "🤝 အၾကံေပးမည္။" }).Select(a => new CardAction
                {
                    Title = a,
                    Type = ActionTypes.ImBack,
                    Value = a
                }).ToList()
            };

            await context.PostAsync(reply);
            context.Wait(MessageReceivedAsync);
        }


    }
}