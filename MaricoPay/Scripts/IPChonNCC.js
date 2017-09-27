function openConfirmationWindow(carID) {
    radWindow1.set_title(carID);
    radWindow1.show();
}
function bookNowCloseClicking(sender, args) {
    radWindow1.close();
    togglePanels();
    radGrid1.get_masterTableView().fireCommand("UpdateCount", radWindow1.get_title());
    args.set_cancel(true);
}
function bookNowClicking(sender, args) {
    togglePanels();
    args.set_cancel(true);
}
function cancelClicking(sender, args) {
    radWindow1.close();
    args.set_cancel(true);
}
function togglePanels() {
    var step1Visible = panelStep1.style.display != "none";
    panelStep1.style.display = step1Visible ? "none" : "";
    panelStep2.style.display = step1Visible ? "" : "none";
}