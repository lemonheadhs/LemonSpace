var gulp = require('gulp');
var less = require('gulp-less');

gulp.task('less', function () {
    gulp.src(['less/*.less', 'less/indetail/*.less'])
        .pipe(less())
        .pipe(gulp.dest(function (f) {
            return f.base.replace('less\\', 'less\\builds\\')
        }));
});

gulp.task('default', function () {
    gulp.watch('less/**/*.less', ['less']);
});

