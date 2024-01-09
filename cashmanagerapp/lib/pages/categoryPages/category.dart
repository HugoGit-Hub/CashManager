import 'package:flutter/material.dart';

class Category extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Catégories'),
      ),
      body: GridView.count(
        crossAxisCount: 2, // Number of buttons per row
        padding: EdgeInsets.all(16.0),
        children: [
          _buildCategoryButton(context, 'Pruduit laitier', '/category/dairy', 'lib/images/dairy.png'),
          _buildCategoryButton(context, 'Fuits', '/category/fruit', 'lib/images/fruit.png'),
          _buildCategoryButton(context, 'Légumes', '/category/vegetable', 'lib/images/vegetable.png'),
          _buildCategoryButton(context, 'Champignons', '/category/mushroom', 'lib/images/mushroom.png'),
          _buildCategoryButton(context, 'Oeufs', '/category/eggs', 'lib/images/eggs.png'),
          _buildCategoryButton(context, 'Céréales', '/category/cereal', 'lib/images/cereal.png'),
          _buildCategoryButton(context, 'Sucreries', '/category/sweet', 'lib/images/sweet.png'),
          // Add more buttons for other categories
        ],
      ),
    );
  }

  Widget _buildCategoryButton(BuildContext context, String categoryName, String route, String imagePath) {
    return InkWell(
      onTap: () {
        Navigator.pushNamed(context, route);
      },
      child: Card(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SizedBox(
              width: 80.0,
              height: 80.0,
              child: Image.asset(
                imagePath,
                fit: BoxFit.cover,
              ),
            ),
            SizedBox(height: 8.0),
            Text(
              categoryName,
              style: TextStyle(fontSize: 16.0, fontWeight: FontWeight.bold),
            ),
          ],
        ),
      ),
    );
  }
}