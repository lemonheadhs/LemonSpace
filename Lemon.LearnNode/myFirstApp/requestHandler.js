//var exec = require("child_process").exec;

function start(response) {
    console.log("Request handler 'start' was called.");
    
    var body = 
        '<html>'+
        '<head>'+
        '<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />'+
        '</head>'+
        '<body>'+
        '<form action="/upload" >'+
        '<textarea name="text" row="20" cols="60"></textarea>'+
        '<input type="submit" value="Submit Text" />'+
        '</form>'+
        '</body>'+
        '</html>';
        
        response.writeHead(200, {"content-type":"text/html"});
        response.write(body);
        response.end();
}

function upload(response) {
    console.log("Request handler 'upload' was called.");
    response.writeHead(200, {"content-type":"text/plain"});
    response.write("Hello Upload");
    response.end();
}

exports.start = start;
exports.upload = upload;