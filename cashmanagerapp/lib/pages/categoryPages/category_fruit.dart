import 'package:flutter/material.dart';

class CategoryFruit extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Category'),
      ),
      body: Center(
        child: Text('This is Category Fruit'),
      ),
    );
  }
}