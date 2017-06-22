"use strict";

// import * as http from 'http';
// import * as route from './route.js';
// import * as socket from './sokcet.js';
const http = require('http');
const route = require('./server/route.js');
const socket = require('./server/socket.js');

let app = http.createServer(route.distribute);
app.listen(9000);

socket.init(app);

