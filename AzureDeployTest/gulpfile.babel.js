'use strict';

import gulp from 'gulp';
import del  from 'del';
import sourcemaps  from 'gulp-sourcemaps';
import uglify from "gulp-uglify";
import browserify  from 'browserify';
import vueify  from 'vueify';
import babelify from "babelify";
import source  from 'vinyl-source-stream';
import buffer from "vinyl-buffer";
import gutil from 'gulp-util';
import less from 'gulp-less';
import path from 'path';

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
    .pipe(uglify())
    .pipe(sourcemaps.init({ loadMaps: true }))
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