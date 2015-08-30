/*This is script for the handling of drag and drop file uploading using HTML5/JS/JQuery
**based on a series of tutorials found at : http://www.sitepoint.com/html5-file-drag-and-drop/
**
*/


//call the initializer function if the browser supports HTML5 file uploading
if (window.File && window.FileList && window.FileReader) {
    Init();
}

function Init() {
    //this is required in order to use datatransfer property on jquery objects
    jQuery.event.props.push('dataTransfer');

    var $filedrag = $('#filedrag');
    var $msg = $('#message');

    var xhr = new XMLHttpRequest();
    //check if browser supports xhr2
    if (xhr.upload) {

        // add event listeners to file drop
        $filedrag.on("dragover", FileDragHover);
        $filedrag.on("dragleave", FileDragHover);
        $filedrag.on("drop", FileSelectHandler);
        //display the file drop box
        $filedrag.css("display", "block");
    }

    //changes the css styling as user drags file over
    function FileDragHover(e) {
        e.stopPropagation();
        e.preventDefault();
        e.target.className = (e.type == "dragover" ? "hover" : "");
    }

    //handler for dropped file
    function FileSelectHandler(e) {

        // cancel browser events and remove hover style
        FileDragHover(e);

        // gets a FileList object containing dropped files
        var files = e.originalEvent.dataTransfer.files;
        //alert(files.item(0).name.toString());
        
        UploadFile(files.item(0));
    }

    //upload a dropped file
    function UploadFile(file) {
       // alert(file.type.toString());
        var xhr = new XMLHttpRequest();
        //ensure browser supports xhr2, and file is a csv file less than 400KB
        if (xhr.upload && (file.type == "text/csv" || file.type == "application/vnd.ms-excel") && file.size <= 400000) {

            var formdata = new FormData();
            formdata.append(file.name, file);
            //upload the file
            xhr.open("POST", "../../Product/CreateFromFile", true);
            xhr.setRequestHeader("X_FILENAME", file.name);            
            xhr.send(formdata);

            //display success message
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    $msg.html("Successfully Created Products From File!")
                }
            }
        }
        else {
            //display fail message
            $msg.html("Invalid File Format/File Too Large"
                + "<br />(File must be in CSV format, and less than 400KB in size.)");
        }
    }

    function Redirect() {
        var redirectUrl = 'Index';
        window.location.href = redirectUrl;
    }

}