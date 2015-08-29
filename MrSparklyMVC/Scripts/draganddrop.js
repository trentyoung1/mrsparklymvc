//call the initializer function
if (window.File && window.FileList && window.FileReader) {
    Init();
}

function Init() {
    //this is required in order to use datatransfer property on jquery objects
    jQuery.event.props.push('dataTransfer');

    var $filedrag = $('#filedrag');

    var xhr = new XMLHttpRequest();
    //check if browser supports xhr2
    if (xhr.upload) {

        // add event listener to file drop
        $filedrag.on("dragover", FileDragHover);
        $filedrag.on("dragleave", FileDragHover);
        $filedrag.on("drop", FileSelectHandler);        
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
        //alert("Hi");
        // cancel event and hover styling
        FileDragHover(e);

        // fetch FileList object
        var files = e.originalEvent.dataTransfer.files;
        //alert(files.item(0).name.toString());
        
        UploadFile(files.item(0));
    }

    //upload a dropped file
    function UploadFile(file) {
       // alert(file.type.toString());
        var xhr = new XMLHttpRequest();
        //ensure browser supports xhr2, and file is a csv file less than 400KB
        if (xhr.upload && (file.type == "text/csv" || file.type == "application/vnd.ms-excel") && file.size <= 4096000) {
            alert(file.type.toString());
            var formdata = new FormData();
            formdata.append(file.name, file);
            //upload the file
            xhr.open("POST", "../../Product/CreateFromFile", true);
            xhr.setRequestHeader("X_FILENAME", file.name);            
            
            xhr.send(formdata);
        }
    }

    function Redirect() {
        var redirectUrl = 'Index';
        window.location.href = redirectUrl;
    }

}