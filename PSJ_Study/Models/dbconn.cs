using System.Data.SqlClient;

namespace PSJ_Study.Models
{
    public class dbconn
    {
        public SqlDataReader ConnectDB(string pSQLQuery, string dbName = "FOODSTOREAPP")
        {
            //접속을 위한 마스터 변수 생성
            SqlConnection conn = new SqlConnection();
            //DB쿼리를 실행시켜줄 마스터 변수 생성
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                //어디 DB에 붙을지 접속정보
                conn.ConnectionString = $"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog={dbName};Data Source=(local)";
                //DB 연결
                conn.Open();
                //실행시킬 쿼리 입력
                sqlComm.CommandText = pSQLQuery;
                sqlComm.Connection = conn;

            }
            catch (Exception ex)
            {

            }

            //실제 쿼리를 DB에서 실행시키고 결과값을 data에 저장
            SqlDataReader data = sqlComm.ExecuteReader();

            return data;
        }
    }
}
