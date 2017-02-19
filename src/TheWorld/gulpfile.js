﻿/// <binding AfterBuild='minify' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var uglify = require('gulp-uglify');

gulp.task('minify', function () {
    // place code for your default task here
    return gulp.src('wwwwroot/js/*.js').pipe(uglify()).pipe("wwwroot/lib/_app");
});