function addPadding(element) {
    element.innerHTML += "<div style=\"height:50px;clear:both;\"></div>";
}
function removePadding(element) {
    element.innerHTML = element.innerHTML.substr(0, element.innerHTML.length-46);
}
export default {addPadding, removePadding};