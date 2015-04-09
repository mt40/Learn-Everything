using KidGame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidGame.Services
{
    /// <summary>
    /// Singleton. Control the app.
    /// </summary>
    public sealed class GeneralService
    {
        private static readonly GeneralService instance = new GeneralService();

        private GeneralService()
        {
            Categories = new ObservableCollection<Category>()
            {
                CreateCategoryAnimal(),
                CreateCategoryFruit(),
                CreateCategoryCountry(),
                CreateCategoryTransportation(),
                CreateCategoryChristmas()
            };

            CurrentUser = new GameUser();
        }

        public static GeneralService Instance
        {
            get { return instance; }
        }

        #region Create Category
        private static Category CreateCategoryChristmas()
        {
            var christmas = new Category("Christmas");
            christmas.DisplayItems = new ObservableCollection<Concept>()
            {
                new Concept("candle", "Christmas", false),
                new Concept("candy", "Christmas", false),
                new Concept("christmas tree", "Christmas", false),
                new Concept("deer", "Christmas", false),
                new Concept("gift", "Christmas", false),
                new Concept("hat", "Christmas", false),
                new Concept("mitten", "Christmas", false),
                new Concept("snowflake", "Christmas", false),
                new Concept("sock", "Christmas", false),
                new Concept("snowman", "Christmas", false),
                new Concept("jingle bell", "Christmas", false),
            };
            christmas.Picture = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Assets/Icons/star.png", UriKind.RelativeOrAbsolute));
            return christmas;
        }

        private static Category CreateCategoryTransportation()
        {
            var transportation = new Category("Transportations");
            transportation.DisplayItems = new ObservableCollection<Concept>()
            {
                new Concept("airplane", "Transportations"),
                new Concept("car", "Transportations"),
                new Concept("ambulance", "Transportations"),
                new Concept("helicopter", "Transportations"),
                new Concept("motorcycle", "Transportations"),
                new Concept("ship", "Transportations", false),
                new Concept("train", "Transportations"),
                new Concept("truck", "Transportations"),
                new Concept("firetruck", "Transportations"),
                new Concept("police car", "Transportations"),
            };
            transportation.Picture = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Assets/Icons/car.png", UriKind.RelativeOrAbsolute));
            return transportation;
        }

        private static Category CreateCategoryCountry()
        {
            var country = new Category("Countries");
            country.DisplayItems = new ObservableCollection<Concept>()
            {
                new Concept("America", "Countries"),
                new Concept("Australia", "Countries"),
                new Concept("Brasil", "Countries"),
                new Concept("Argentina", "Countries"),
                new Concept("China", "Countries"),
                new Concept("Canada", "Countries"),
                new Concept("Germany", "Countries"),
                new Concept("Japan", "Countries"),
                new Concept("Greece", "Countries"),
                new Concept("Italia", "Countries"),
                new Concept("Spain", "Countries"),
                new Concept("Sweden", "Countries"),
                new Concept("Russia", "Countries"),
                new Concept("France", "Countries"),
            };
            country.Picture = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Assets/Icons/flag.png", UriKind.RelativeOrAbsolute));
            return country;
        }

        private static Category CreateCategoryFruit()
        {
            var fruit = new Category("Fruits");
            fruit.DisplayItems = new ObservableCollection<Concept>()
            {
                new Concept("apple", "Fruits", false),
                new Concept("apricot", "Fruits", false),
                new Concept("banana", "Fruits", false),
                new Concept("cherry", "Fruits", false),
                new Concept("kiwi", "Fruits", false),
                new Concept("lemon", "Fruits", false),
                new Concept("mango", "Fruits", false),
                new Concept("orange", "Fruits", false),
                new Concept("peach", "Fruits", false),
                new Concept("pear", "Fruits", false),
                new Concept("strawberry", "Fruits", false),
            };
            fruit.Picture = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Assets/Icons/fruit.png", UriKind.RelativeOrAbsolute));
            return fruit;
        }

        private static Category CreateCategoryAnimal()
        {
            var animal = new Category("Animals");
            animal.DisplayItems = new ObservableCollection<Concept>()
            {
                new Concept("dog", "Animals"),
                new Concept("bird", "Animals"),
                new Concept("cat", "Animals"),
                new Concept("duck", "Animals"),
                new Concept("dolphin", "Animals"),
                new Concept("fish", "Animals"),
                new Concept("horse", "Animals"),
                new Concept("frog", "Animals"),
                new Concept("mouse", "Animals"),
                new Concept("pig", "Animals"),
                new Concept("shark", "Animals", false),
                new Concept("cow", "Animals"),
            };
            animal.Picture = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Assets/Icons/parrot.png", UriKind.RelativeOrAbsolute));
            return animal;
        }
        #endregion

        public Category CurrentCategory { get; set; }

        public GameUser CurrentUser { get; set; }

        public GameMode CurrentMode { get; set; }

        public ObservableCollection<Category> Categories;

        public static Question GetRandomQuestion()
        {
            var service = GeneralService.Instance;
            var list = service.CurrentCategory.DisplayItems;
            //get a random first concept
            var concept = list.FirstOrDefault();
            //get the first 4 items
            var tmp = list.Take(4).Select(x => x).ToList().Shuffle(UtilityService.GlobalRandom);
            var choices = new ObservableCollection<Concept>(tmp);

            var question = new Question()
            {
                Answer = list.First(),
                Choices = choices
            };

            return question;
        }

    }
}
