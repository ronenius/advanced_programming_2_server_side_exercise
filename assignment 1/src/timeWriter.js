function getTime(x) {
    if (x.hour<10 && x.minute<10)
            return "0"+x.hour+":0"+x.minute;
        else if (x.hour<10)
            return "0"+x.hour+":"+x.minute;
        else if (x.minute<10)
            return x.hour+":0"+x.minute;
        return x.hour+":"+x.minute;
}
function getISOtime(s) {
    let time = new Date(s);
    return getTime({
        hour:time.getHours(),
        minute:time.getMinutes(),
        day:time.getDate(),
        month:time.getMonth() + 1,
        year:time.getFullYear(),
    });
}
function getDate(x) {
    return x.day+"/"+x.month;
}
function getISOdate(s) {
    let time = new Date(s);
    return getDate({
        hour:time.getHours(),
        minute:time.getMinutes(),
        day:time.getDate(),
        month:time.getMonth() + 1,
        year:time.getFullYear(),
    })
}
export default {getTime, getDate, getISOtime, getISOdate};