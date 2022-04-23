using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PSJ_Study.Models;

namespace PSJ_Study.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            List<COUNTRY> list = new List<COUNTRY>();
            COUNTRY cnt = new COUNTRY();
            cnt.IDX = 0;
            cnt.CODE = "KOR";
            cnt.COUNTRY2 = "한식";
            cnt.REGDATE = DateTime.Now;
            cnt.ISUSE = true;
            list.Add(cnt);

            dbconn dbconn = new dbconn();
            var data = dbconn.ConnectDB("SELECT * FROM COUNTRY", "FOODAPP");

            DateTime dt = DateTime.Now;
            //여러줄의 쿼리를 한줄씩 foreach 해줌
            while (data.Read())
            {
                list.Add(new COUNTRY()
                {
                    IDX = (int)data["IDX"],
                    CODE = data["CODE"].ToString(),
                    COUNTRY2 = data["COUNTRY"].ToString(),
                    REGDATE = DateTime.TryParse(data["REGDATE"].ToString(), out dt) ? dt : DateTime.Now,
                    ISUSE = data["ISUSE"] == "Y" ? true : false
                });
                //쿼리값을 가져오기 json과 비슷한 방식
                string colum1 = data["IDX"].ToString();
                string colum2 = data["COUNTRY"].ToString();
            }

            ViewBag.list = JsonConvert.SerializeObject(list);

            return View();
        }

        public IActionResult FoodApp()
        {
            dbconn dbconn = new dbconn();

            Dictionary<string, Dictionary<string, StoreData>> appData 
                = new Dictionary<string, Dictionary<string, StoreData>>();

            Dictionary<int, FOOD_COUNTRY> country_dict = new Dictionary<int, FOOD_COUNTRY>(); 
            string query_country = "SELECT * FROM COUNTRY";
            var data_country = dbconn.ConnectDB(query_country);
            while (data_country.Read())
            {
                FOOD_COUNTRY country = new FOOD_COUNTRY() {
                    IDX = (int)data_country["IDX"],
                    KOR_COUNTRY = data_country["KOR_COUNTRY"].ToString()
                };
                country_dict.Add(country.IDX, country);
            }

            List<STORE> store_list = new List<STORE>();
            foreach(var country in country_dict)
            {
                string query_store = $"SELECT * FROM STORE WHERE COUNTRY_IDX = {country.Value.IDX}";
                var data_store = dbconn.ConnectDB(query_store);
                while (data_store.Read())
                {
                    store_list.Add(new STORE() 
                    { 
                        IDX = (int)data_store["IDX"],
                        COUNTRY_IDX = (int)data_store["COUNTRY_IDX"],
                        KOR_NAME = data_store["KOR_NAME"].ToString(),
                        TIP = (int)data_store["TIP"],
                        MIN_TIME = (int)data_store["MIN_TIME"],
                        MAX_TIME = (int)data_store["MAX_TIME"],
                        SCORE = (int)data_store["SCORE"]
                    });
                }
            }

            
            foreach (var store in store_list)
            {
                string country = country_dict[store.COUNTRY_IDX].KOR_COUNTRY;
                if (!appData.ContainsKey(country)) appData.Add(country, new Dictionary<string, StoreData>());
                
                string query_food = $"SELECT * FROM FOOD_DETAIL WHERE STORE_IDX = {store.IDX}";
                var data_food = dbconn.ConnectDB(query_food);

                List<FOOD_DETAIL> food_list = new List<FOOD_DETAIL>();
                while (data_food.Read())
                {
                    food_list.Add(new FOOD_DETAIL()
                    {
                        IDX = (int)data_food["IDX"],
                        STORE_IDX = (int)data_food["STORE_IDX"],
                        KOR_NAME = data_food["KOR_NAME"].ToString(),
                        PRICE = (int)data_food["PRICE"]
                    });
                }

                StoreData storeData = new StoreData();
                storeData.GetData(store, food_list);

                appData[country].Add(store.KOR_NAME, storeData);
            }

            ViewBag.foodAppData = JsonConvert.SerializeObject(appData);

            return View();
        }

        public string GetCountry()
        {

            return "";
        }
    }
}
