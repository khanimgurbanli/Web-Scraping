
using Test.Services;

string url = "https://tr.tommy.com/kadin-giyim?p=0";

IProductService _productService = new ProductService();
var getResponse = await _productService.CopyProductFromWebsiteAsync(url);

var response = await _productService.CreateProductAsync(getResponse);



//IWebDriver driver = new ChromeDriver();
//driver.Navigate().GoToUrl("https://tr.tommy.com/kadin-giyim");

//List<string[]> items = new List<string[]>();
//IReadOnlyCollection<IWebElement> productElements = driver.FindElements(By.CssSelector(".p-item"));

//foreach (IWebElement productElement in productElements)
//{
//    string name = productElement.FindElement(By.CssSelector(".p-name span")).Text;
//    string price = productElement.FindElement(By.CssSelector(".one-price")).Text;

//    items.Add(new string[] { name, price });
//}

//driver.Quit();

//string csvFilePath = "D:\\Task\\Test\\items.csv";
//using (StreamWriter writer = new StreamWriter(csvFilePath))
//{
//    writer.WriteLine("Name,Price");
//    foreach (string[] item in items)
//    {
//        writer.WriteLine(string.Join(",", item));
//    }
//}