function route(handle, pathname, response) {
    if (typeof handle[pathname] === "function") {
        console.log("About to route a request for "+pathname);
        handle[pathname](response);
    } else {
        console.log("No request handler found for "+pathname);
        response.writeHead(404, {"content-type": "text/plain"});
        response.write("404 not found");
        response.end();
    }
}

exports.route = route;