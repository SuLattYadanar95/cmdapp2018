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


    public enum ReminderTypes
    {
        [Describe("👌 Daily")]
        [Terms(new string[] { "👌 Daily", "daily" })]
        Daily,
        [Describe("👌 Weekly")]
        [Terms(new string[] { "👌 Weekly", "weekly" })]
        Weekly       
       
    };

    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "I do not understand \"{0}\".", "Try again, I don't get \"{0}\".")]
    [Template(TemplateUsage.DateTimeHelp, "This field should be in the format '20 Sep 2018'", "Please enter a date or time")]
    public class ReminderFormFlowDialog
    {

        private string _name;
        [Template(TemplateUsage.EnumSelectOne, "What do you want me to remind you? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string ActivityType;
       
        public string Activity;
        [Template(TemplateUsage.String, "How often do you want me to remind you? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public ReminderTypes ReminderType;
        public string Schedule;
        [Template(TemplateUsage.EnumSelectOne, "What is your preference 🖖 {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string Frequency;
        [Template(TemplateUsage.EnumSelectOne, "What is your preference 🖖 {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string Time;
        [Template(TemplateUsage.EnumSelectOne, "What is your preference 🖖 {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string Until;
        [Template(TemplateUsage.EnumSelectOne, "Are you going to get our gift? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public ConfirmOptions? Confirmation;

        [Optional]
        [Template(TemplateUsage.NoPreference, "None")]
        public string Notes;


        public ReminderFormFlowDialog(string name)
        {
           _name = name;
        }
        public static IForm<ReminderFormFlowDialog> BuildForm(string name)
        {
            return new FormBuilder<ReminderFormFlowDialog>()
            .Message($"Hi {name}, Caremebot is going to give you special gift. '{name}' is right for your name and okay to use for future ?")
            .Field(new FieldReflector<ReminderFormFlowDialog>(nameof(ActivityType))
                .SetType(null)
                .SetDefine(async (state, field) =>
                {

                    foreach (var item in ResourceHelper.activityTypes)
                    {
                        field
                            .AddDescription(item.Name, item.Name, item.Image, item.Description)
                            .AddTerms(item.Name, item.Name);
                    }

                    return true;
                }))
            .Field(new FieldReflector<ReminderFormFlowDialog>(nameof(ActivityType))
                .SetDefine(async (state, field) => {
                    if (state.ActivityType == "Pill reminder")
                    {
                        field.SetPrompt(new PromptAttribute("Any medicine to remind you?. Mention your medicine. (e.g Amoxicillin)"));
                    }
                    else if (state.ActivityType == "Exercise reminder")
                    {
                        field.SetPrompt(new PromptAttribute("Select activity to remind you? {||}") { ChoiceStyle = ChoiceStyleOptions.Buttons });
                        foreach (var item in ResourceHelper.exerciseTypes)
                        {
                            field
                                .AddDescription(item.Name, item.Name, item.Image, item.Description)
                                .AddTerms(item.Name, item.Name);
                        }
                    }
                    return true;
                }))
            .Field(nameof(ReminderType), (state) => state.ActivityType != null ? true : false)
            .Field(new FieldReflector<ReminderFormFlowDialog>(nameof(Schedule))
                .SetDefine(async (state, field) => {
                    if (state.ReminderType == ReminderTypes.Daily)
                    {
                        field.SetPrompt(new PromptAttribute("How often everyday I need to remind you. (e.g Amoxicillin) {||}") { ChoiceStyle = ChoiceStyleOptions.Buttons });
                        foreach (var item in ResourceHelper.exerciseTypes)
                        {
                            field
                                .AddDescription(item.Name, item.Name, item.Image)
                                .AddTerms(item.Name, item.Name);
                        }
                    }
                    else if (state.ActivityType == "Exercise reminder")
                    {
                        field.SetPrompt(new PromptAttribute("Any medicine to remind you?. Mention your medicine. (e.g Amoxicillin) {||}") { ChoiceStyle = ChoiceStyleOptions.Buttons });
                        foreach (var item in ResourceHelper.exerciseTypes)
                        {
                            field
                                .AddDescription(item.Name, item.Name, item.Image, item.Description)
                                .AddTerms(item.Name, item.Name);
                        }
                    }
                    return true;
                }))
            .Field(new FieldReflector<ReminderFormFlowDialog>(nameof(Confirmation))
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
                        if (field.Name == nameof(ActivityType))// && prompt.Buttons.Count <= 11)
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

        private static async Task<bool> DefinitionMethod(ReminderFormFlowDialog state, Field<ReminderFormFlowDialog> field)
        {
            field.SetPrompt(new PromptAttribute($"You chose a rating of {state.ActivityType}. What is your name?."));
            return true;
        }

    };

}