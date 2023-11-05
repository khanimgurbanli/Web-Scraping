using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Test.Dtos;
using CreateCategory_Task.Contexts;
using Microsoft.EntityFrameworkCore;
using Test.Entities;
using Test.Helpers;

namespace Test.Services
{
    public class ProductService : IProductService
    {
        Entity_Context _context = new Entity_Context();

        public async Task<List<CreateRequestDto>> CopyProductFromWebsiteAsync(string url)
        {
            string productName = string.Empty;
            string productPrice = string.Empty;
            string productLink = string.Empty;
            string imagePath = string.Empty;
            string categoryName = url.Get();

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("listColumn")));

            List<IWebElement> productElements = new List<IWebElement>();

            List<CreateRequestDto> requestDto = new();

            for (int pageCount = 0; pageCount < 10; pageCount++)
            {
                var currentProductElements = driver.FindElements(By.ClassName("p-item"));
                productElements.AddRange(currentProductElements);

                foreach (var productElement in currentProductElements)
                {
                    var productNameElement = productElement.FindElement(By.CssSelector(".p-name a"));
                    productName = productNameElement.GetAttribute("title");

                    var productLinkElement = productElement.FindElement(By.CssSelector(".p-name a"));
                    productLink = productLinkElement.GetAttribute("href");

                    var productPriceElement = productElement.FindElement(By.CssSelector(".p-price")); ;
                    productPrice = productPriceElement.GetAttribute("textContent");

                    //var productImagePathElement = productElement.FindElement(By.CssSelector(".p-image position-relative picture"));
                    //imagePath = productImagePathElement.Text;

                    requestDto.Add(new() { Name = productName, Price = productPrice, ProductLink = productLink, ImagePath = imagePath, CategoryName = categoryName });

                    Console.WriteLine($"Name: {productName}\nPrice: {productPrice}\nCategory: {categoryName}\nProductLink: {productLink}"  );
                }

                var nextPageElement = driver.FindElement(By.CssSelector(".pagernext-new.active"));
                if (nextPageElement != null)
                {
                    string nextPageUrl = nextPageElement.GetAttribute("href");

                    if (!string.IsNullOrEmpty(nextPageUrl))
                    {
                        driver.Navigate().GoToUrl(nextPageUrl);
                    }
                }

                wait.Until(ExpectedConditions.StalenessOf(currentProductElements[0]));
            }

            Console.WriteLine("Count" + productElements.Count);

            //string csvFilePath = "D:\\Task\\Test\\products.csv";
            //using (StreamWriter writer = new StreamWriter(csvFilePath))
            //{
            //    writer.WriteLine("Name,Price");
            //    foreach (var item in productElements)
            //    {
            //        writer.WriteLine(string.Join(",", item));
            //    }
            //}
            return requestDto;
        }

        public DbSet<Product> Table => _context.Set<Product>();

        public async Task<bool> CreateProductAsync(List<CreateRequestDto> model)
        {
            var products = model.Select(x => new Product
            {
                Name = x.Name,
                Price = x.Price,
                ProductLink = x.ProductLink,
                ImagePath = x.ImagePath,
                CategoryName = x.CategoryName
            }).ToList();

            await Table.AddRangeAsync(products);

            int recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }
    }
}
