var functions = require('./functions');
var pagesary = functions.getPages(false);
var express = require('express');
var path = require('path');
var app = express();

app.use('/assets', express.static(path.join(__dirname, '/src/assets')));
app.get('/help', function(req, res) {
    res.send('Hello!');
});

// app.set('view engine', 'ejs');
// app.set('views', path.join(__dirname, '/src/'));

// app.get('/', function(req, res) {
//     var jspdata = JSON.parse(JSON.stringify(pagesary)).filter(x => x.userOptions.filename.includes("index"))[0];
//     res.render('index', { title: jspdata.userOptions.title });
// });

app.listen(3001);
console.log('Server is listening at port 3001');