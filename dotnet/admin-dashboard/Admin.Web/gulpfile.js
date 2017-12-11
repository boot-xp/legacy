var gulp = require('gulp');

gulp.task('copy-fonts', function () {
    return gulp.src([
        './node_modules/bootstrap/dist/fonts/**/*.*'
    ]).pipe(gulp.dest('./wwwroot/fonts'));
});

gulp.task('copy-styles', function ()  {
    return gulp.src([
        './node_modules/bootstrap/dist/css/bootstrap.min.css',
        './node_modules/bootstrap/dist/css/bootstrap-theme.min.css',
        './styles/styles.css'
    ]).pipe(gulp.dest('./wwwroot/styles'));
});

gulp.task('copy-scripts', function ()  {
    return gulp.src([
        './node_modules/jquery/dist/jquery.min.js',
        './node_modules/bootstrap/dist/js/bootstrap.min.js',
        './node_modules/mustache/mustache.min.js',
        './scripts/index.js'
    ]).pipe(gulp.dest('./wwwroot/scripts'));
});

gulp.task('default', ['copy-scripts', 'copy-styles', 'copy-fonts']);