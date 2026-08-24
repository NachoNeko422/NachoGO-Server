namespace NachoGO_Server
{
    public static class RoomManager
    {
        //用来管理房间的类，为了多开房间和查询房间
        public static Dictionary<string, CreatRoom> Rooms = new();
    }
}
