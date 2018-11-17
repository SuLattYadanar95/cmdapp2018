using Data.Models;
using Infra.Helper;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Scorables.Internals;
using Microsoft.Bot.Connector;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaremebotMSApi.Scorable
{
    public class CancelScorable : ScorableBase<IActivity, string, double>
    {
        private readonly IDialogTask task;

        public CancelScorable(IDialogTask task)
        {
            SetField.NotNull(out this.task, nameof(task), task);
        }

        protected override async Task<string> PrepareAsync(IActivity activity, CancellationToken token)
        {
            var message = activity as IMessageActivity;
            await ActivityLogApiRequestHelper.CreateOrEdit(new tbActivityLog { UserId = message.From.Id, UserName = message.From.Name, EventPayload = message.Text });


            if (message != null && !string.IsNullOrWhiteSpace(message.Text))
            {
                if (new string[] { "exit", "fuck", "start over", "back", "home", "ေတာ္ျပီ", "ရပ္", "ထြက္မည္", "ျပန္စ", "ရျပီ" }.Contains(message.Text.ToLower()))
                {
                    return message.Text;
                }
            }

            return null;
        }

        protected override bool HasScore(IActivity item, string state)
        {
            return state != null;
        }

        protected override double GetScore(IActivity item, string state)
        {
            return 1.0;
        }

        protected override async Task PostAsync(IActivity item, string state, CancellationToken token)
        {
           
            this.task.Reset();
        }
        protected override Task DoneAsync(IActivity item, string state, CancellationToken token)
        {
            
            return Task.CompletedTask;
        }
    }
}