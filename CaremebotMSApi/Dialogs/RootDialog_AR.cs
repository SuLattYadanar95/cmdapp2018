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
                var obj = await HospitalApiRequestHelper.GetHospitalById(ResourceHelper.hospitalId);
                var reply = msg.CreateReply($"Welcome to {obj.Name}. This is testing bot and I am here to help you.");
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                reply.Attachments.Add(new HeroCard
                {
                    Title = obj.Name,
                    Text = obj.Description,
                    Images = new List<CardImage> { new CardImage(obj.WelcomePhotoUrl) },
                    Buttons = new List<CardAction>
                        {
                            new CardAction(ActionTypes.ImBack, "Health Tip", value:"Health Tip"),
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
                context.Wait(MessageReceivedAsync);
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
            else if (msg.Text.ToLower().Equals("services"))
            {
                var objs = await ServiceApiRequestHelper.Get(hospitalid: 1);
                var reply = msg.CreateReply($"You are welcome. Learn about our services.");
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                var attachments = new List<Attachment>() {
                        new HeroCard
                        {
                            Title = "Book doctor",
                            Text = "Check and make booking",
                            Images = new List<CardImage> { new CardImage(ResourceHelper.buy_ticket_img_url) },
                            Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.OpenUrl, "Book now", value:"https://airlineticketbotclient.yammobots.com/home/index"),

                            }
                        }.ToAttachment(),
                        new HeroCard
                        {
                            Title = "Suggest me",
                            Text = "Let us know your pain and get suggestion",
                            Images = new List<CardImage> { new CardImage(ResourceHelper.ticket_chat_img_url) },
                            Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.ImBack, "Suggest me", value:"Suggest me"),

                            }
                        }.ToAttachment(),
                    };
                if (objs != null && objs.Count > 0)
                {
                    attachments.AddRange(objs.Select(a => new HeroCard
                    {
                        Title = a.Title,
                        Text = a.Description,
                        Images = new List<CardImage> { new CardImage(a.ImageUrl) },
                        Buttons = new List<CardAction>
                        {
                            new CardAction(ActionTypes.OpenUrl, "Learn more", value:$"https://airlineticketbotclient.yammobots.com/domain/index?id={a.ID}&airlineid={1}"),
                            new CardAction(ActionTypes.OpenUrl, "Contact now", value:$"tel:{a.Phone}")
                        }
                    }).ToList().Select(a => a.ToAttachment()).ToList());
                }
                reply.Attachments = attachments;
                await context.PostAsync(reply);



            }
            else if (msg.Text.ToLower().Equals("suggest me"))
            {
                var hospitals = await HospitalApiRequestHelper.Get(pagesize: 0);
                var bodyparts = await BodyPartApiRequestHelper.Get(pagesize: 0);
                var form = new FormDialog<EngFormFlowDialog>(new EngFormFlowDialog(bodyparts, hospitals), EngFormFlowDialog.BuildForm, FormOptions.None, null);
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