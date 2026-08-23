using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NachoGO_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetDataController : ControllerBase
    {
        [HttpPost("CreatRoom")]
        public IActionResult CreatRoom([FromBody] string playerID)
        {
            CreatRoom room = new CreatRoom();

            // 生成房间号
            room.RandomRoomID();

            // 保存房间
            RoomManager.Rooms.Add(room.RoomID, room);


            return Ok(room.RoomID);
        }

        [HttpPost("JoinRoom")]
        public IActionResult JoinRoom(string name, string playerID, int team, string roomID)
        {
            // 根据房间号寻找房间
            if (!RoomManager.Rooms.TryGetValue(roomID, out CreatRoom? room))
            {
                return NotFound("Failed");
            }

            // 找到了指定房间
            int statusPlayerID =room.AddData(room.PlayerID, playerID);
            int statusName =room.AddData(room.Name, name);
            room.Team = team;

            if (statusPlayerID == 1 || statusName == 1)
            {
                return Ok("RoomFull");
            }

            return Ok("Successful");
        }
    }
}
