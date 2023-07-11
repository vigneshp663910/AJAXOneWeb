
var wrapper = document.getElementById("signature-pad"),
    //clearButton = wrapper.querySelector("[data-action=clear]"),
    //saveButton = wrapper.querySelector("[data-action=save]"),

    saveButton = document.getElementById("MainContent_UC_ICTicketView_btnSignSave"),
    //saveButton= document.getElementById("btnSign"),
     
    canvas = wrapper.querySelector("canvas"),
    signaturePad;

var wrapper1 = document.getElementById("signature1-pad"), 
    canvas1 = wrapper1.querySelector("canvas"),
    signaturePad1;

// Adjust canvas coordinate space taking into account pixel ratio,
// to make it look crisp on mobile devices.
// This also causes canvas to be cleared.
function resizeCanvas() {
    // When zoomed out to less than 100%, for some very strange reason,
    // some browsers report devicePixelRatio as less than 1
    // and only part of the canvas is cleared then.
    var ratio = Math.max(window.devicePixelRatio || 1, 1);
    //canvas.width = canvas.offsetWidth * ratio;
    //canvas.height = canvas.offsetHeight * ratio;

    canvas.width = 320;
    canvas.height = 320;
    canvas.getContext("2d").scale(ratio, ratio);

    canvas1.width = 320;
    canvas1.height = 320;
    canvas1.getContext("2d").scale(ratio, ratio);
}

window.onresize = resizeCanvas;
//resizeCanvas();

signaturePad = new SignaturePad(canvas);
signaturePad1 = new SignaturePad(canvas1);

//clearButton.addEventListener("click", function (event) {
//    signaturePad.clear();
//});

saveButton.addEventListener("click", function (event) {
    //if (signaturePad.isEmpty()) {
    //    //alert("Please provide signature first.");
    //} else {
    debugger;
    //var lblProcessID = document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessID").value
    //if (lblProcessID = 2) {
    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_hfTSign").value = signaturePad.toDataURL();
    //  document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessID").value = "3";
    //  document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessName").value = "Photo Of Customer";
    // }
    // else if (lblProcessID = 4) {
    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_hfCSign").value = signaturePad1.toDataURL();
    //    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessID").value = "5";
    //   document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessName").value = "Name Of Customer Signature";
    // }  
    // }
});
 