
function openWin(url) {
    var strWindowFeatures = "menubar=no,location=yes,resizable=yes,scrollbars=yes,status=yes,height=400,width=520";
    return window.open(url, "", strWindowFeatures);
}

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});





