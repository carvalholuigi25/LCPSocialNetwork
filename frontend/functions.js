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

  return [
    new HtmlWebpackPlugin({
      title: "LCP Social Network",
      filename: "index.html",
      template: "./src/index.ejs",
      chunks: ['main'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Admin Dashboard",
      filename: "pages/admin/dashboard.html",
      template: "./src/pages/admin/dashboard.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - About",
      filename: "pages/about.html",
      template: "./src/pages/about.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Chat",
      filename: "pages/chat.html",
      template: "./src/pages/chat.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Friends",
      filename: "pages/friends.html",
      template: "./src/pages/friends.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Groups",
      filename: "pages/groups.html",
      template: "./src/pages/groups.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Images",
      filename: "pages/images.html",
      template: "./src/pages/images.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Login",
      filename: "pages/login.html",
      template: "./src/pages/login.ejs",
      chunks: ['main'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Main",
      filename: "pages/main.html",
      template: "./src/pages/main.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Notifications",
      filename: "pages/notifications.html",
      template: "./src/pages/notifications.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Posts",
      filename: "pages/posts.html",
      template: "./src/pages/posts.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Privacy Policy",
      filename: "pages/privacy_policy.html",
      template: "./src/pages/privacy_policy.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Profile",
      filename: "pages/profile.html",
      template: "./src/pages/profile.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Register",
      filename: "pages/register.html",
      template: "./src/pages/register.ejs",
      chunks: ['main'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Reset your Account",
      filename: "pages/reset_account.html",
      template: "./src/pages/reset_account.ejs",
      chunks: ['main'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Settings",
      filename: "pages/settings.html",
      template: "./src/pages/settings.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Terms of Service",
      filename: "pages/terms_of_service.html",
      template: "./src/pages/terms_of_service.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    }),
    new HtmlWebpackPlugin({
      title: "LCP Social Network - Videos",
      filename: "pages/videos.html",
      template: "./src/pages/videos.ejs",
      chunks: ['pages'],
      minify: optmini,
      base: getBase(),
      year: year
    })
  ];
}

module.exports = { getBase, setMinifyForWPConfig, getPages };