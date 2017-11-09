'use strict';

const gulp = require('gulp');
const del = require('del');
const sourcemaps  = require('gulp-sourcemaps');
const uglify = require("gulp-uglify");
const browserify = require('browserify');
const vueify = require('vueify');
const babelify = require("babelify");
const source = require('vinyl-source-stream');
const buffer = require("vinyl-buffer");
const gutil = require('gulp-util');
const less = require('gulp-less');
const path = require('path');

gulp.task('default', ['clean'], function () {
    gulp.start(['build','less']);
});

gulp.task('clean', function () {
    return del('./js/dst/');
});

gulp.task('build', () => {
    browserify({
        entries: './js/src/vueComponents/app.jsx',
        extensions: ['.jsx'],
        debug: true
    })
    .transform(vueify)
    .transform(babelify)
    .bundle()
    .on('error', err => {
        gutil.log("Browserify Error", gutil.colors.red(err.message));
    })
    .pipe(source('index.js'))
    .pipe(buffer())
    .pipe(sourcemaps.init({ loadMaps: true }))
    .pipe(uglify())
    .pipe(sourcemaps.write('./maps'))
    .pipe(gulp.dest('./js/dst'));
});


gulp.task('less', function () {
    return gulp.src('./js/src/less/**/*.less')
        .pipe(less({
            paths: [ path.join(__dirname, 'less', 'includes') ]
        }))
        .pipe(gulp.dest('./js/dst'));
});