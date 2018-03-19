var connect = require('connect');
var serveStatic = require('serve-static');
require('dotenv').config()

var port = process.env.PORT || 80
connect().use(serveStatic(__dirname)).listen(port, function(){
    console.log('Server running on '+port);
});