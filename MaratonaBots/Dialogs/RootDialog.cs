using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MaratonaBots.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync("*** Olá tudo bem?***");

            var message = activity.CreateReply();


            if (activity.Text.Equals("herocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var heroCard = CreateHeroCard();
                message.Attachments.Add(heroCard);

            }
            else if (activity.Text.Equals("videocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var videoCard = CreateVideoCard();
                message.Attachments.Add(videoCard);

            }
            else if (activity.Text.Equals("audiocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var audioCard = CreateAudioCard();
                message.Attachments.Add(audioCard);

            }
            else if (activity.Text.Equals("animationcard", StringComparison.InvariantCultureIgnoreCase))
            {
                var animationCard = CreateAnimationCard();
                message.Attachments.Add(animationCard);

            }
            else if (activity.Text.Equals("carousel", StringComparison.InvariantCultureIgnoreCase))
            {

                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                var audio = CreateAudioCard();
                var animate = CreateAnimationCard();

                message.Attachments.Add(audio);
                message.Attachments.Add(animate);
            }
            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

        private Attachment CreateAnimationCard()
        {
            var animationcard = new AnimationCard();

            animationcard.Title = "Um gif revelador";
            animationcard.Subtitle = "Outro titulo";
            animationcard.Autostart = true;
            animationcard.Autoloop = false;
            animationcard.Media = new List<MediaUrl> {
                    new MediaUrl("http://www.reactiongifs.com/r/hsk.gif")
                };

            return animationcard.ToAttachment();
        }

        private Attachment CreateAudioCard()
        {
            var audiocard = new AudioCard();

            audiocard.Title = "Um audio revelador";
            audiocard.Image = new ThumbnailUrl("https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Gnome-stock_person_bot.svg/1024px-Gnome-stock_person_bot.svg.png");
            audiocard.Subtitle = "Outro titulo";
            audiocard.Autostart = true;
            audiocard.Autoloop = false;
            audiocard.Media = new List<MediaUrl> {
                    new MediaUrl("https://www.w3schools.com/html/horse.mp3")
                };
            return audiocard.ToAttachment();
        }

        private Attachment CreateVideoCard()
        {
            var videoCard = new VideoCard();

            videoCard.Title = "Um video Qualquer";
            videoCard.Subtitle = "Outro titulo";
            videoCard.Autostart = true;
            videoCard.Autoloop = false;
            videoCard.Media = new List<MediaUrl> {
                    new MediaUrl("http://techslides.com/demos/sample-videos/small.mp4")
                };

            return videoCard.ToAttachment();
        }

        private Attachment CreateHeroCard()
        {
            var heroCard = new HeroCard();

            heroCard.Title = "Planeta";
            heroCard.Subtitle = "Universo";

            heroCard.Images = new List<CardImage> {
                new CardImage("https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Gnome-stock_person_bot.svg/1024px-Gnome-stock_person_bot.svg.png","Bot", new CardAction(ActionTypes.OpenUrl,value: "htttps://www.google.com"))
            };

            heroCard.Buttons = new List<CardAction> {
                new CardAction
                {
                    Text = "Texto",
                    Title = "Titulo",
                    DisplayText = "Display",
                    Type = ActionTypes.PostBack,
                    Value = "Aqui vai um valor"

                }
            };
            return heroCard.ToAttachment();
        }
    }
}