import 'package:flutter/material.dart';

class CategoryFruit extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Catégories'),
      ),
      body: Center(
        child: Text('Voici la page de la catégorie fruits !'),
      ),
    );
  }
}
