using UnderstandingStructureApp.Interface;
using UnderstandingStructureApp.Models;
using UnderstandingStructureApp.Exceptions;
namespace UnderstandingStructureApp.Repositories
{
    public class PizzaImageRepository : IRepository<int, PizzaImages>
    {
        List<PizzaImages> images = new List<PizzaImages>
        {
            new PizzaImages(){Id = 1, Images = new List<string>(){ "https://th.bing.com/th/id/OIP.8UeIFPMYwIErE1ShRYB9QAHaEo?rs=1&pid=ImgDetMain", "https://wallup.net/wp-content/uploads/2017/11/22/371886-food-pizza.jpg", "https://th.bing.com/th/id/R.a90f420ad8c71c3aacf28f87de3f1708?rik=A5WC8v2va1qFGg&riu=http%3a%2f%2f4.bp.blogspot.com%2f-n-jZjyEzncE%2fUq8IxN6-giI%2fAAAAAAAADWk%2fOL-YhSPEG_4%2fs1600%2fPizza%2bFood%2bHd%2bWallpaper.jpg&ehk=P%2f8NwSvvwjLvNExGqNkmVRip5lATlUieTUwxOT6m2D0%3d&risl=&pid=ImgRaw&r=0" } },
            new PizzaImages(){Id = 2, Images = new List<string>(){ "https://th.bing.com/th/id/OIP._Tuj6ElUF8jhhcSg41_V_QHaE8?rs=1&pid=ImgDetMain", "https://www.simplyrecipes.com/thmb/rLl58QZmVP4C3zSlpkKBo72EUws=/2000x1333/filters:fill(auto,1)/__opt__aboutcom__coeus__resources__content_migration__simply_recipes__uploads__2019__09__easy-pepperoni-pizza-lead-3-8f256746d649404baa36a44d271329bc.jpg", "https://th.bing.com/th/id/OIP.GZwtdeRcBzrJ6Di5a70kYAHaEx?rs=1&pid=ImgDetMain" } }
        };
        public PizzaImages Add(PizzaImages item)
        {
            if (item.Images.Count == 0)
                throw new CannotAddWithNoImagesException();
            images.Add(item);
            return item;
        }

        public PizzaImages Delete(int key)
        {
            var myImages = Get(key);
            if (myImages == null)
                throw new NoSuchImageException();
            images.Remove(myImages);
            return myImages;
        }

        public PizzaImages Get(int key)
        {
            var image = images.FirstOrDefault(i => i.Id == key);
            if (image == null)
                throw new NoSuchImageException();
            return image;
        }

        public List<PizzaImages> GetAll()
        {
            if (images.Count == 0)
                throw new NoImagesException();
            return images;
        }

        public PizzaImages Update(PizzaImages pizza)
        {
            if (pizza.Images.Count == 0)
                throw new CannotUpdateWithNoImagesException();
            var myImages = Get(pizza.Id);
            if (myImages == null)
                throw new NoSuchImageException();
            myImages.Images = pizza.Images;
            return myImages;
        }
    }
}
