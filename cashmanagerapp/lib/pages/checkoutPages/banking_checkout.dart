import 'package:flutter/material.dart';
import 'package:webview_flutter/webview_flutter.dart';

class BankingCheckout extends StatefulWidget {
  const BankingCheckout({super.key});

  @override
  State<BankingCheckout> createState() => _BankingCheckout();
}

class _BankingCheckout extends State<BankingCheckout> {
  final controller = WebViewController()
  ..setJavaScriptMode(JavaScriptMode.unrestricted)
  ..loadRequest(Uri.parse('https://cash-manager-nine.vercel.app/'));
  
  var loadingPercentage = 0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Banking Checkout'),
      ),
      body : WebViewWidget(controller: controller),
    );
  }
}