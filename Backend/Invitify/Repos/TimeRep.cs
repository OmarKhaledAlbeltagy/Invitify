using Invitify.Entities;
using Invitify.Models;
namespace Invitify.Repos
{
    public class TimeRep : ITimeRep
    {

        public DateTime GetCurrentTime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, PropertiesModel.TimeZone);
            var res = _localTime - new DateTime(_localTime.Year, _localTime.Month, _localTime.Day);
            return _localTime;
        }
    }
}
