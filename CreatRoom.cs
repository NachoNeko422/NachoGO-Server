namespace NachoGO_Server
{
    public class CreatRoom
    {
        //目前CreatRoom类只有生成房间号和添加数据的功能
        public string RoomID { get; set; }
        public string[] Name { get; set; } = new string[10];
        public string[] PlayerID { get; set; } = new string[10];
        public int Team { get; set; }

        public void RandomRoomID()
        {
            Random random = new Random();
            string randomNumber = random.Next(100000, 1000000).ToString(); //生成六位随机房间号

            RoomID = randomNumber;
        }



        int status; //0为成功 1为失败
        public int AddData(string[] array, string data)
        {
            
            for (int i = 0; i < 10; i++)
            {
                if (array[i] == null)
                {
                    array[i] = data;
                    status = 0;
                    break;
                }
                else
                {
                    status = 1;
                }
            }
            return status;
        }
    }
}
