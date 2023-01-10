var HtmlWebpackPlugin = require("html-webpack-plugin");

function getBase() {
  return {
    href: process.env.NODE_ENV == 'production' ? '/dist/' : '/'
  };
}

function setMinifyForWPConfig() {
  return {
    removeAttributeQuotes: false,
    collapseWhitespace: true,
    removeComments: true
  };
}

function getPages(isProd = false) {
  var optmini = isProd ? setMinifyForWPConfig() : { removeAttributeQuotes: false };
  var year = new Date().getUTCFullYear();
  var ptitle = "LCP Social Network";

  return [
    new HtmlWebpackPlugin({
      title: ptitle,
      filename: "index.html",
      template: "./src/index.ejs",
      chunks: ['main'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Admin Dashboard",
      filename: "pages/admin/dashboard.html",
      template: "./src/pages/admin/dashboard.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - About",
      filename: "pages/about.html",
      template: "./src/pages/about.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Chat",
      filename: "pages/chat.html",
      template: "./src/pages/chat.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Friends",
      filename: "pages/friends.html",
      template: "./src/pages/friends.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Groups",
      filename: "pages/groups.html",
      template: "./src/pages/groups.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Images",
      filename: "pages/images.html",
      template: "./src/pages/images.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Login",
      filename: "pages/login.html",
      template: "./src/pages/login.ejs",
      chunks: ['main'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Main",
      filename: "pages/main.html",
      template: "./src/pages/main.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Notifications",
      filename: "pages/notifications.html",
      template: "./src/pages/notifications.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Posts",
      filename: "pages/posts.html",
      template: "./src/pages/posts.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Privacy Policy",
      filename: "pages/privacy_policy.html",
      template: "./src/pages/privacy_policy.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Profile",
      filename: "pages/profile.html",
      template: "./src/pages/profile.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Profile Edit",
      filename: "pages/profile/edit.html",
      template: "./src/pages/profile/edit.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Profile Report",
      filename: "pages/profile/report.html",
      template: "./src/pages/profile/report.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Post Edit",
      filename: "pages/post/edit.html",
      template: "./src/pages/post/edit.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Post Delete",
      filename: "pages/post/delete.html",
      template: "./src/pages/post/delete.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Register",
      filename: "pages/register.html",
      template: "./src/pages/register.ejs",
      chunks: ['main'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Reset your Account",
      filename: "pages/reset_account.html",
      template: "./src/pages/reset_account.ejs",
      chunks: ['main'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Settings",
      filename: "pages/settings.html",
      template: "./src/pages/settings.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Terms of Service",
      filename: "pages/terms_of_service.html",
      template: "./src/pages/terms_of_service.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Videos",
      filename: "pages/videos.html",
      template: "./src/pages/videos.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Animes",
      filename: "pages/userthings/animes.html",
      template: "./src/pages/userthings/animes.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Comic Books",
      filename: "pages/userthings/comicbooks.html",
      template: "./src/pages/userthings/comicbooks.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Games",
      filename: "pages/userthings/games.html",
      template: "./src/pages/userthings/games.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Mangas",
      filename: "pages/userthings/mangas.html",
      template: "./src/pages/userthings/mangas.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - Movies",
      filename: "pages/userthings/movies.html",
      template: "./src/pages/userthings/movies.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: ptitle + " - TV Series",
      filename: "pages/userthings/tvseries.html",
      template: "./src/pages/userthings/tvseries.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    })
  ];
}

module.exports = { getBase, setMinifyForWPConfig, getPages };