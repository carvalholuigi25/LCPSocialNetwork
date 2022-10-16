const path = require("path");
var htmlWebpackPlugin = require("html-webpack-plugin");

module.exports = {
  entry: {
    main: "./src/scripts/main.js",
    pages: "./src/scripts/pages.js"
  },
  module: {
    rules: [
      {
        test: /\.html$/i,
        use: ["html-loader"]
      },
      {
        test: /\.(woff|woff2|ttf|eot)$/i,
        type: 'asset/resource',
        generator: {
          filename: 'assets/fonts/[name][ext][query]'
        }
      },
      {
        test: /\.(7z|zip|rar)$/i,
        type: 'asset/resource',
        generator: {
          filename: 'assets/files/[name][ext][query]'
        }
      },
      {
        test: /\.(mp4|mov|flv)$/i,
        type: 'asset/resource',
        generator: {
          filename: 'assets/videos/[name][ext][query]'
        }
      },
      {
        test: /\.(svg|png|jpe?g|gif|webp)$/i,
        type: 'asset/resource',
        generator: {
          filename: 'assets/images/[name][ext][query]'
        }
      },
      {
        test: /\.ico$/i,
        type: 'asset/resource',
        generator: {
          filename: 'assets/icons/[name][ext][query]'
        }
      }
    ]
  }
};