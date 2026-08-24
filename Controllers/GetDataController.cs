using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NachoGO_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetDataController : ControllerBase
    {
        [HttpPost("CreatRoom")]
        public IActionResult CreatRoom()
        {
            CreatRoom room = new CreatRoom();

            // 生成房间号
            room.RandomRoomID();

            // 保存房间
            RoomManager.Rooms.Add(room.RoomID, room);


            return Ok(room.RoomID);
        }

        [HttpPost("JoinRoom")]
        public IActionResult JoinRoom([FromBody] string[] data)
        {
            string name = data[0];
            string playerID = data[1];
            string roomID = data[2];

            // 根据房间号寻找房间
            if (!RoomManager.Rooms.TryGetValue(roomID, out CreatRoom room))
            {
                return NotFound("Failed");
            }

            // 找到了指定房间
            int statusPlayerID =room.AddData(room.PlayerID, playerID);
            int statusName =room.AddData(room.Name, name);

            if (statusPlayerID == 1 || statusName == 1)
            {
                return Ok("RoomFull");
            }

            return Ok($"Successful:{roomID}");
        }
        //开始游戏
        [HttpPost("RunGame")]
        public IActionResult RunGame([FromBody] int team)
        {
            //开始游戏的逻辑，对接CSGO服务端
            return Ok();
        }

        //上传头像
        [HttpPost("UploadAvatar")]
        public IActionResult UploadAvatar([FromBody] AvatarUploadData data)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(),"Avatar"); //定义头像存储的文件夹路径
            
            if (!Directory.Exists(folder))
            {
                //路径没有就创建
                Directory.CreateDirectory(folder);
            }

            string path = Path.Combine(folder, Path.GetFileName(data.FileName)); //定义包含头像名的绝对路径

            System.IO.File.WriteAllBytes(path,data.Data);

            return Ok("AvatarSuccess");
        }
        public class AvatarUploadData
        {
            public string FileName { get; set; }
            public byte[] Data { get; set; }
        }
    }
}
