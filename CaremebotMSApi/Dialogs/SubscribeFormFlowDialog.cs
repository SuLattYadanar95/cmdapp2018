using CaremebotMSApi.Helper;
using Data.Helper;
using Data.Models;
using Infra.Helper;
using LinqKit;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CaremebotMSApi.Dialogs
{


    public enum OccuranceOptions
    {
        [Describe("👌 Weekly")]
        [Terms(new string[] { "👌 Weekly", "👌 weekly", "weekly", "once a week" })]
        Weekly,
        [Describe("👌 Monthly")]
        [Terms(new string[] { "👌 Monthly", "👌 monthly", "monthly", "once a month" })]
        Monthly,
        [Describe("👌 At anytime")]
        [Terms(new string[] { "👌 At anytime", "👌 at anytime", "anytime" })]
        Anytime
       
    };
    
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "I do not understand \"{0}\".", "Try again, I don't get \"{0}\".")]
    [Template(TemplateUsage.DateTimeHelp, "This field should be in the format '20 Sep 2018'", "Please enter a date or time")]
    public class SubscribeFormFlowDialog
    {

        private string _name;
        [Template(TemplateUsage.EnumSelectOne, "If it is your name say 'Yes'. otherwise, say 'No' {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public ConfirmOptions? NameConfirmation;
        public string Name;
        [Template(TemplateUsage.String, "Okay. Your contact number 🤙?")]
        public string PhoneNumber;
        [Template(TemplateUsage.EnumSelectOne, "What is your preference 🖖 {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string SubscriptionType;
        [Template(TemplateUsage.EnumSelectOne, "😊 How often you want to receive our material? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public OccuranceOptions? OccuranceOption;
        [Template(TemplateUsage.EnumSelectOne, "Are you going to get our gift? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public ConfirmOptions? Confirmation;

        [Optional]
        [Template(TemplateUsage.NoPreference, "None")]
        public string Notes;


        public SubscribeFormFlowDialog(string name)
        {
           _name = name;
        }
        public static IForm<SubscribeFormFlowDialog> BuildForm(string name)
        {
            return new FormBuilder<SubscribeFormFlowDialog>()
            .Message($"Hi {name}, Caremebot is going to give you special gift. '{name}' is right for your name and okay to use for future ?")
            .Field(nameof(NameConfirmation))
            .Field(nameof(Name), "So, let me know your name to contact you in future.", (state) => state.NameConfirmation == ConfirmOptions.No ? true : false,
                validate: async (state, response) => 
                {
                    var result = new ValidateResult { IsValid = true, Value = response };
                    var value = (response as string).Trim();
                    if (value.Length < 1)
                    {
                        result.Feedback = "Please provide your name.";
                        result.IsValid = false;
                    }
                    return result;
                })

            .Field(new FieldReflector<SubscribeFormFlowDialog>(nameof(SubscriptionType))
                .SetType(null)
                .SetDefine(async (state, field) =>
                {
                    if (state.NameConfirmation != null)
                    {
                        
                        foreach (var item in ResourceHelper.subscriptionTypes)
                        {
                            field
                                .AddDescription(item.Name, item.Name, item.Image)
                                .AddTerms(item.Name, item.Name);
                        }
                    }
                    return true;
                }))
            .Field(nameof(OccuranceOption), (state) => state.SubscriptionType != null ? true : false)
            .Field(new FieldReflector<SubscribeFormFlowDialog>(nameof(Confirmation))
                .SetNext((value, state) =>
                {
                    var selection = (ConfirmOptions)value;
                    if (selection == ConfirmOptions.No)
                    {
                        state.Name = null;
                        state.SubscriptionType = null;
                        state.OccuranceOption = null;
                        state.Confirmation = null;
                        return new NextStep(StepDirection.Reset);
                    }
                    else
                    {
                        return new NextStep();
                    }
                }))
           
            .OnCompletion(async (context, state) =>
            {
                await context.PostAsync("Thanks. See you again.");
            })
            .Prompter(async (context, prompt, state, field) =>
            {

                var message = context.Activity as Activity;
                IMessageActivity promptMessage = context.MakeMessage();
                // Handle buttons as quick replies when possible (FB only renders 11 quick replies)
                if (field != null)
                {

                    if (prompt.Buttons.Count > 0)// && prompt.Buttons.Count <= 11)
                    {
                        if (field.Name == nameof(SubscriptionType))// && prompt.Buttons.Count <= 11)
                        {
                            // Build a standard prompt with suggested actions. 

                            promptMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                            promptMessage.Attachments = prompt.Buttons.Select(a => new HeroCard
                            {
                                Title = a.Description,
                                Subtitle = a.Message,
                                Images = new List<CardImage> { new CardImage(a.Image) },
                                Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.ImBack, "🤤 Subscribe",null,a.Description)
                            }
                            }).ToList().Select(a => a.ToAttachment()).ToList();

                        }
                        
                        else
                        {
                            var actions = prompt.Buttons.Select(button => new CardAction
                            {
                                Type = ActionTypes.ImBack,
                                Title = button.Description,
                                Value = button.Description
                            }).ToList();
                            promptMessage.SuggestedActions = new SuggestedActions(actions: actions);
                        }
                        
                    }

                    else if (prompt.Description != null)
                    {
                        var preamble = context.MakeMessage();
                        if (prompt.GenerateMessages(preamble, promptMessage))
                        {
                            await context.PostAsync(preamble);
                        }

                    }
                }
                if (prompt != null)
                {
                    promptMessage.Text = prompt.Prompt ?? default(string);

                }

                await context.PostAsync(promptMessage);
                return prompt;
            })
            .Build();

        }



    };

}