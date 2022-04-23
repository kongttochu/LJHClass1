namespace PSJ_Study.Models
{
    public class MainClass
    {

    }

    public class COUNTRY
    {
        public int IDX { get; set; }
        public string COUNTRY2 { get; set; }
        public string CODE { get; set; }
        public DateTime REGDATE { get; set; }
        public bool ISUSE { get; set; }

    }

    public class FOOD_COUNTRY
    {
        public int IDX { get; set; }
        public string KOR_COUNTRY { get; set; }
    }

    public class STORE
    {
        public int IDX { get; set; }
        public int COUNTRY_IDX { get; set; }
        public string KOR_NAME { get; set; }
        public int TIP { get; set; }
        public int MIN_TIME { get; set; }
        public int MAX_TIME { get; set; }
        public int SCORE { get; set; }
    }

    public class FOOD_DETAIL
    {
        public int IDX { get; set; }
        public int STORE_IDX { get; set; }
        public string KOR_NAME { get; set; }
        public int PRICE { get; set; }
    }

    public class StoreData
    {
        public int tip { get; set; }
        public int minTime { get; set; }
        public int maxTime { get; set; }
        public int score { get; set; }
        public IDictionary<string, int> food { get; set; }

        public void GetData(STORE store, List<FOOD_DETAIL> foodList)
        {
            tip = store.TIP;
            minTime = store.MIN_TIME;
            maxTime = store.MAX_TIME;
            score = store.SCORE;
            food = new Dictionary<string, int>();
            foreach (var foodDetail in foodList)
            {
                food.Add(foodDetail.KOR_NAME
                    , foodDetail.PRICE);
            }
        }
    }
}
