import timeWriter from "./timeWriter.js"
function addMessage(x,name, username, id) {
    if (x.sent)
        return "<div id=\""+username+"message#"+id+"\" class=\"speech-wrapper\">\n<div class=\"bubble\">\n<div class=\"txt\">\n<p class=\"name\">"+"You"+"</p>\n<p class=\"message\">"+x.contance+"</p>\n<span class=\"timestamp\">"+timeWriter.getTime(x)+"</span>\n</div>\n<div class=\"bubble-arrow\"></div>\n</div></div>\n";
    else
        return "<div id=\""+username+"message#"+id+"\" class=\"speech-wrapper\">\n<div style=\"width:100%; position:relative\">\n<div class=\"bubble alt\">\n<div class=\"txt\">\n<p class=\"name alt\">"+name+"</p>\n<p class=\"message\">"+x.contance+"</p>\n<span class=\"timestamp\">"+timeWriter.getTime(x)+"</span>\n</div>\n<div class=\"bubble-arrow alt\"></div>\n</div></div></div>\n";
}
export default addMessage;