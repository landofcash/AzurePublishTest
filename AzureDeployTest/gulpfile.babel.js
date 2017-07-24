'use strict';

var _gulp = require('gulp');

var _gulp2 = _interopRequireDefault(_gulp);

var _del = require('del');

var _del2 = _interopRequireDefault(_del);

var _gulpSourcemaps = require('gulp-sourcemaps');

var _gulpSourcemaps2 = _interopRequireDefault(_gulpSourcemaps);

var _gulpUglify = require('gulp-uglify');

var _gulpUglify2 = _interopRequireDefault(_gulpUglify);

var _browserify = require('browserify');

var _browserify2 = _interopRequireDefault(_browserify);

var _vueify = require('vueify');

var _vueify2 = _interopRequireDefault(_vueify);

var _babelify = require('babelify');

var _babelify2 = _interopRequireDefault(_babelify);

var _vinylSourceStream = require('vinyl-source-stream');

var _vinylSourceStream2 = _interopRequireDefault(_vinylSourceStream);

var _vinylBuffer = require('vinyl-buffer');

var _vinylBuffer2 = _interopRequireDefault(_vinylBuffer);

var _gulpUtil = require('gulp-util');

var _gulpUtil2 = _interopRequireDefault(_gulpUtil);

var _gulpLess = require('gulp-less');

var _gulpLess2 = _interopRequireDefault(_gulpLess);

var _path = require('path');

var _path2 = _interopRequireDefault(_path);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

_gulp2.default.task('default', ['clean'], function () {
    _gulp2.default.start(['build', 'less']);
});

_gulp2.default.task('clean', function () {
    return (0, _del2.default)('./js/dst/');
});

_gulp2.default.task('build', function () {
    (0, _browserify2.default)({
        entries: './js/src/vueComponents/app.jsx',
        extensions: ['.jsx'],
        debug: true
    }).transform(_vueify2.default).transform(_babelify2.default).bundle().on('error', function (err) {
        _gulpUtil2.default.log("Browserify Error", _gulpUtil2.default.colors.red(err.message));
    }).pipe((0, _vinylSourceStream2.default)('index.js')).pipe((0, _vinylBuffer2.default)()).pipe((0, _gulpUglify2.default)()).pipe(_gulpSourcemaps2.default.init({ loadMaps: true })).pipe(_gulpSourcemaps2.default.write('./maps')).pipe(_gulp2.default.dest('./js/dst'));
});

_gulp2.default.task('less', function () {
    return _gulp2.default.src('./js/src/less/**/*.less').pipe((0, _gulpLess2.default)({
        paths: [_path2.default.join(__dirname, 'less', 'includes')]
    })).pipe(_gulp2.default.dest('./js/dst'));
});