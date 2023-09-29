"use strict";

var _platformBrowserDynamic = require("@angular/platform-browser-dynamic");
var _app = require("./app/app.module");
/// <reference types="@angular/localize" />

(0, _platformBrowserDynamic.platformBrowserDynamic)().bootstrapModule(_app.AppModule).catch(function (err) {
  return console.error(err);
});