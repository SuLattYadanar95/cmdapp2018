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

    public enum ConfirmOptions
    {
        [Describe("👌 Yes")]
        [Terms(new string[] { "y", "Y", "Yeah", "Yay", "Yes", "👌 Yes" })]
        Yes,
        [Describe("👎 No")]
        [Terms(new string[] { "n", "N", "No", "Nope", "No", "👎 No" })]
        No
    };
    public enum RateOptions
    {
        [Describe("👌 Not relevant")]
        [Terms(new string[] { "👌 Not relevant","not relevant", "Funny","funny", "Not useful","not useful" })]
        NotRelevant,
        [Describe("👎 Relevant")]
        [Terms(new string[] { "👎 Relevant","Relevant", "Fine","fine", "Okay","okay" })]
        Relevant,
        [Describe("👎 Helpful")]
        [Terms(new string[] { "👎 Helpful", "Helpful","Great","great","Useful","useful" })]
        Userful,
        [Describe("👎 Very Helpful")]
        [Terms(new string[] { "👎 Very Helpful","Very useful","very useful","So great","so great" })]
        VeryUseful
    };
    public enum SeverityOptions
    {
        [Describe("☝️ Very mild")]
        [Terms(new string[] { "☝️ Very mild", "Just something", "Not confortable", "Not so bad" })]
        Single,
        [Describe("✌️ Mild")]
        [Terms(new string[] { "✌️ Mild","Not okay", "Feeling bad" })]
        Round,
        [Describe("✌️ Moderate")]
        [Terms(new string[] { "✌️ Moderate","Feeling pain", "Mild pain" })]
        Moderate,
        [Describe("✌️ Severe")]
        [Terms(new string[] { "✌️ Severe","Can't stand", "So bad", "So painful" })]
        Severe,
        [Describe("✌️ Very severe")]
        [Terms(new string[] { "✌️ Very severe","Can't breath", "So so bad", "Can't stand anymore", "Deadly" })]
        VerySevere
    };
    public enum AgeOptions
    {
        [Describe("👌 Under 10 yrs")]
        [Terms(new string[] { "<10", "<10 years", "<10 yrs","👌 Under 10 yrs","under 10 yrs", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" })]
        Under10,
        [Describe("👌 Between 10 and 21 yrs")]
        [Terms(new string[] { "<21", "<21 years", "<18 yrs", "👌 Between 10 and 21 yrs","Between 10 and 21 yrs", "1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18", "19", "20", })]
        Between1021,
        [Describe("👍 Between 21 and 40 yrs")]
        [Terms(new string[] { "👍 Between 21 and 40 yrs", "between 18 and 40 yrs","21", "22","23","24","25","26","27","28","29","30","31","32","33","34","35","36","37","38","39","40" })]
        Between2140,
        [Describe("👍 Between 41 and 65 yrs")]
        [Terms(new string[] { "👍 Between 41 and 65 yrs", "between 41 and 65 yrs","41","42","43","44","45","46","47","48","49","50","51","52","53","54","55","56","57","58","59","60","61","62","63","64","65" })]
        Between4065,
        [Describe("👍 Over 65 yrs")]
        [Terms(new string[] { ">65", ">65 years", ">65 yrs", "👍 Over 65 yrs", "over 65 years" })]
        Over65
    };
    public enum SeatChoiceOptions
    {
        [Describe("👔 Economy")]
        [Terms(new string[] { "Economy", "economy" })]
        Economy,
        [Describe("💼 Business")]
        [Terms(new string[] { "Business", "business" })]
        Business
    };
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "I do not understand \"{0}\".", "Try again, I don't get \"{0}\".")]
    [Template(TemplateUsage.DateTimeHelp, "This field should be in the format '20 Sep 2018'", "Please enter a date or time")]
    public class EngFormFlowDialog
    {


        private static List<tbHospital> _hospitals;
        private static List<string> _specialties;
        private static List<tbBodyPart> _bodyParts;
        [Template(TemplateUsage.String, "May I know your name to start 😇?")]
        public string Name;
        [Pattern(@"(<Undefined control sequence>\d)?\s*\d{3}(-|\s*)\d{4}")]
        [Template(TemplateUsage.String, "Okay. Your contact number 🤙?")]
        public string PhoneNumber;
        [Template(TemplateUsage.EnumSelectOne, "Let's start with selecting age group you are in. 🖖 {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public AgeOptions? Age;
        [Template(TemplateUsage.EnumSelectOne, "😊 Could you select your bodypart where you feel pain or unconfortable or feeling wrong? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string BodyPart;
        [Template(TemplateUsage.EnumSelectOne, "Any major symptom that you feel in your {BodyPart} or you can also say something about it. {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string Symptom1;
        [Template(TemplateUsage.EnumSelectOne, "How do you feel about {Symptom1} now? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public SeverityOptions? Severity1;
        [Template(TemplateUsage.EnumSelectOne, "Okay thanks. Any problem besides feeling {Symptom1}? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string Symptom2;
        [Template(TemplateUsage.EnumSelectOne, "Going well or so bad? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public SeverityOptions? Severity2;
        [Template(TemplateUsage.EnumSelectOne, "Where do you prefer to visit? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string Hospital;
        [Template(TemplateUsage.EnumSelectOne, "In {Hospital}, I would suggest you to show with these doctors {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public string Doctor;
        
        [Template(TemplateUsage.EnumSelectOne, "Thanks. 😎 I am good enough or helpful for you, honestly you can rate me? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public RateOptions? rate;
        [Template(TemplateUsage.EnumSelectOne, "{||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public ConfirmOptions? Confirmation;

        [Optional]
        [Template(TemplateUsage.NoPreference, "None")]
        public string Notes;


        public EngFormFlowDialog(List<tbBodyPart> bodyParts, List<tbHospital> hospitals)
        {
             _bodyParts = bodyParts;
            _hospitals = hospitals;
        }
        public static IForm<EngFormFlowDialog> BuildForm()
        {
            return new FormBuilder<EngFormFlowDialog>()
            .Message("Welcome! I am a bot and I keep learning to improve myself. Anyway I will help you for your problem. I think I can give you good suggestion to show with appropriate doctors. You can exit or stop the conversation at anytime by entering 'exit' or 'stop'")
            .Field(nameof(Name),
                validate: async (state, response) =>
                {
                    var result = new ValidateResult { IsValid = true, Value = response };
                    var name = (response as string).Trim();
                    if (name == null || name.Length < 0)
                    {
                        result.Feedback = "There should be your name for successful order.";
                        result.IsValid = false;
                    }
                    return result;
                })

            .Field(nameof(PhoneNumber), "Let me know your phone number 🤙🏻?",
                validate: async (state, response) =>
                {
                    var result = new ValidateResult { IsValid = true, Value = response };
                    var value = (response as string).Trim();
                    if (value.Length > 0 && (value[0] < '0' || value[0] > '9'))
                    {
                        result.Feedback = "Phone must start with a number.";
                        result.IsValid = false;
                    }
                    return result;
                })
            .Field(nameof(Age))
            .Field(new FieldReflector<EngFormFlowDialog>(nameof(BodyPart))
                .SetType(null)
                .SetActive((state) => (state.Age != null ? true : false))
                .SetDefine(async (state, field) =>
                {
                    if (state.Age != null)
                    {
                        var objs = _bodyParts.Select(a => a.BodyPart).Distinct().OrderBy(a => a).ToList();
                        foreach (var item in objs)
                        {
                            field
                                .AddDescription(item, item)
                                .AddTerms(item, item);
                        }
                    }
                    return true;
                }))
             .Field(new FieldReflector<EngFormFlowDialog>(nameof(Symptom1))
                .SetType(null)
                .SetActive((state) => (state.BodyPart != null ? true : false))
                .SetDefine(async (state, field) =>
                {
                    if (state.BodyPart != null)
                    {
                        var objs = _bodyParts.Where(a => a.BodyPart.StartsWith(state.BodyPart)).Select(a => a.Symptom_English).OrderBy(a => a).ToList();
                        foreach (var item in objs)
                        {
                            field
                                .AddDescription(item, item)
                                .AddTerms(item, item);
                        }
                    }
                    return true;
                }))
            .Field(nameof(Severity1))
            .Field(new FieldReflector<EngFormFlowDialog>(nameof(Symptom2))
                .SetType(null)
                .SetActive((state) => (state.BodyPart != null ? true : false))
                .SetDefine(async (state, field) =>
                {
                    if (state.BodyPart != null)
                    {
                        var objs = _bodyParts.Where(a => a.BodyPart.StartsWith(state.BodyPart)).Select(a => a.Symptom_English).OrderBy(a => a).ToList();
                        objs.Insert(0, "No other symptoms");
                        foreach (var item in objs)
                        {
                            field
                                .AddDescription(item, item)
                                .AddTerms(item, item);
                        }
                    }
                    return true;
                }))
            //.Field(new FieldReflector<EngFormFlowDialog>(nameof(Severity2))
            //    .SetType(null)
            //    .SetActive((state) => state.Symptom2 != "No other symptoms" ? true : false))
            .Field(nameof(Severity2),active: (state) => (state.Symptom2 != "No other symptoms" ? true : false))
            .Field(new FieldReflector<EngFormFlowDialog>(nameof(Hospital))
                .SetType(null)
                .SetActive((state) => (state.BodyPart != null && state.Symptom1 != null ? true : false))
                .SetDefine(async (state, field) =>
                {
                    if (state.Severity1 != null && state.Symptom2 != null)
                    {
                        _specialties = string.Join(",", _bodyParts.Where(a => a.Symptom_English.ToLower().Contains(state.Symptom1.ToLower()) || a.Symptom_English.ToLower().Contains(state.Symptom2.ToLower()))
                            .Select(a => a.Specialty).ToList()).Split(',').Distinct().ToList();

                        if (state.Age == AgeOptions.Under10 || state.Age == AgeOptions.Between1021)
                        {
                            _specialties.Insert(0, "Paediatrician");
                        }
                        var predicate = PredicateBuilder.New<tbHospital>();
                        if (state.BodyPart != null)
                        {

                            foreach (var info in _specialties)
                            {
                                predicate = predicate.Or(b => (b.Specialty ?? string.Empty).ToLower().Contains((info ?? string.Empty).ToLower()));
                            }
                            var objs = _hospitals.Where(predicate).OrderBy(a => a.Name).Take(10).ToList();
                            foreach (var item in objs)
                            {
                                field
                                    .AddDescription(item.Name, item.Name, item.PhotoUrl, $"{item.ID}_{item.Township}")
                                    .AddTerms(item.Name, item.Name);
                            }
                        }
                    }
                    return true;
                }))
            .Field(new FieldReflector<EngFormFlowDialog>(nameof(Doctor))
                .SetType(null)
                .SetActive((state) => (state.Hospital != null ? true : false))
                .SetDefine(async (state, field) =>
                {
                    if (state.Hospital != null)
                    {

                        var objs = await DoctorApiRequestHelper.Get(hospitalname: state.Hospital, specialties: string.Join("_", _specialties));

                        if (objs != null)
                        {
                            foreach (var item in objs.Results)
                            {
                                field
                                    .AddDescription(item.Name, item.Name, item.PhotoUrl, $"{item.ID}_{item.Qualification}")
                                    .AddTerms(item.Name, item.Name);
                            }
                        }
                    }
                    return true;
                })
                 .SetNext((value, state) =>
                 {                    
                         return new NextStep(StepDirection.Complete);

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
                        if (field.Name == nameof(Hospital))// && prompt.Buttons.Count <= 11)
                        {
                            // Build a standard prompt with suggested actions. 

                            promptMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                            promptMessage.Attachments = prompt.Buttons.Select(a => new HeroCard
                            {
                                Title = a.Description,
                                Subtitle = a.Message.Split('_')[1],
                                Images = new List<CardImage> { new CardImage(a.Image) },
                                Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.ImBack, "🤤 View doctor",null,a.Description)
                            }
                            }).ToList().Select(a => a.ToAttachment()).ToList();

                        }
                        else if (field.Name == nameof(Doctor))
                        {
                            promptMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                            promptMessage.Attachments = prompt.Buttons.Select(a => new HeroCard
                            {
                                Title = a.Description,
                                Subtitle = a.Message.Split('_')[1],
                                Images = new List<CardImage> { new CardImage(a.Image) },
                                Buttons = new List<CardAction>
                            {
                                new CardAction(ActionTypes.OpenUrl, "🤤 Get appointment ",null,value: $"http://caremebotclient.azurewebsites.net/appointment/index?doctorid={a.Message.Split('_')[0]}&msgrid=123123&msgrname={state.Name}")
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