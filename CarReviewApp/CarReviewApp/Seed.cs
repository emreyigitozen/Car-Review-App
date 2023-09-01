using CarReviewApp.Data;
using CarReviewApp.Models;

namespace CarReviewApp
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext dataContext)
        {
            _context = dataContext;
        }

        public void SeedDataContext()
        {
            if (!_context.CarOwners.Any())
            {
                var carowners = new List<CarOwner>() {
                 
                  new CarOwner
                  {
                      Car=new Car
                      {
                          Name="Clio 1.5 Dci",
                          CarCategories=new List<CarCategory>()
                          {
                              new CarCategory{Category=new Category{Name="Renault"}}
                          },
                          Review=new List<Review>
                          {
                              new Review{Title="Araba çok iyi",Description="Rengini çok beğendim."},
                              new Review{Title="Araba çok kötü",Description="Motoru Çok Kötü"}

                          }
                      },
                      Owner=new Owner
                      {
                          Name="Emre"
                      }


                  },
                  new CarOwner
                  {
                      Car=new Car
                      {
                          Name="Corsa",
                          CarCategories=new List<CarCategory>()
                          {
                              new CarCategory{Category=new Category{Name="Opel",}}
                          },
                          Review=new List<Review>
                          {
                              new Review{Title="Rengi çok iyi",Description="Corsanın vites geçişleri çok güzel"},
                              new Review{Title="Ucuza Aldım",Description="Yüzde 10 indirimle aldım."}
                          }
                      },
                      Owner=new Owner
                      {
                          Name="Onur"
                      }
                      
                  },
                  new CarOwner
                  {
                      Car=new Car
                      {
                          Name="Egea",
                          CarCategories=new List<CarCategory>()
                          {
                             new CarCategory{Category=new Category{Name="Fiat"} } },
                          Review=new List<Review>
                          {
                              new Review{Title="Fiat almayın",Description="1.4leri çok kötü"},
                              new Review{Title="Fiat pişmanlıktır.",Description="Araba kağıt gibi"}
                          }

                      },
                      Owner=new Owner
                      {
                          Name="Samet"
                      }
                      
                  }


                };
                _context.CarOwners.AddRange(carowners);
                _context.SaveChanges();
            }

        }



    }
}
