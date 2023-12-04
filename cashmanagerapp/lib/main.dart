import 'package:cashmanagerapp/pages/cartPages/cart.dart';
import 'package:cashmanagerapp/pages/categoryPages/category_dairy.dart';
import 'package:cashmanagerapp/pages/categoryPages/category_fruit.dart';
import 'package:cashmanagerapp/pages/categoryPages/category_vegetable.dart';
import 'package:flutter/material.dart';
import 'pages/scanPages/scan_home.dart';
import 'pages/categoryPages/category.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {

  final ScanHome scanHome = ScanHome();
  final Category category = Category();
  final CategoryDairy categoryDairy = CategoryDairy();
  final CategoryVegetable categoryVegetable = CategoryVegetable();
  final CategoryFruit categoryFruit = CategoryFruit();
  final Cart cart = Cart();


  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'CashManager',
      theme: ThemeData(
          useMaterial3: true,
          colorScheme: ColorScheme.fromSeed(seedColor: Colors.orange),
        ),
      initialRoute: '/',
      routes: {
        '/': (context) => HomePage(),
        '/scanHome': (context) => scanHome,
        '/category': (context) => category,
        '/category/fruit' : (context) => categoryFruit,
        '/category/vegetable' : (context) => categoryVegetable,
        '/category/dairy' : (context) => categoryDairy,
        '/cart' : (context) => cart,
      },
    );
  }
}

class HomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        toolbarHeight: 40,
        title: Text('Home',style: TextStyle(fontWeight: FontWeight.bold,fontSize: 20)),
      ),
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          // Row 1
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: Text('Welcome',style: TextStyle(fontWeight: FontWeight.bold,fontSize: 18)),
              ),
              IconButton(
                icon: Icon(Icons.notifications),
                onPressed: () {
                  // Handle notification button press
                },
              ),
            ],
          ),
          SizedBox(height: 10), // Add spacing
          // Row 2
          Container(
            height: 75, // Adjust the height as needed
            margin: EdgeInsets.symmetric(horizontal: 20),
            child: ElevatedButton(
              onPressed: () {
                navigateToPage(context, '/scanHome');
              },
              style: ElevatedButton.styleFrom(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12)
                )
              ),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Row(
                    children: [
                      Icon(Icons.qr_code,size: 50,color: Colors.black), // Add barcode icon
                      SizedBox(width: 8), // Add spacing between icon and text
                      Text('Scan Mode',style: TextStyle(fontSize: 24,color: Colors.black)),
                    ],
                  ),
                  // Add any additional widgets you might need on the right side
                ],
              ),
            ),
          ),
          SizedBox(height: 10), // Add spacing
          // Row 3
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: Text('Category',style: TextStyle(fontWeight: FontWeight.bold,fontSize: 18)),
              ),
              TextButton(
                onPressed: () {
                  navigateToPage(context, '/category');
                },
                child: Text('More'),
              ),
            ],
          ),
          SizedBox(height: 10), // Add spacing
          // Row 4
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: [
              ElevatedButton(
                onPressed: () {
                  navigateToPage(context, '/category/fruit');
                },
                child: Text('Fruit'),
              ),
              ElevatedButton(
                onPressed: () {
                  navigateToPage(context, '/category/vegetable');
                },
                child: Text('Vegetable'),
              ),
              ElevatedButton(
                onPressed: () {
                  navigateToPage(context, '/category/dairy');
                },
                child: Text('Dairy'),
              ),
            ],
          ),
          SizedBox(height: 10), // Add spacing
          // Row 5
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: Text('Cart',style: TextStyle(fontWeight: FontWeight.bold,fontSize: 18)),
              ),
              TextButton(
                onPressed: () {
                  navigateToPage(context, '/cart');
                },
                child: Text('More'),
              ),
            ],
          ),
          SizedBox(height: 10), // Add spacing
          // Row 6 (Placeholder for the list of items)
          Expanded(
            child: ListView.builder(
              itemCount: 5,
              itemBuilder: (context, index) {
                return Container(
                  margin: EdgeInsets.symmetric(vertical: 8, horizontal: 5), // Add space between items
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(20), // Adjust the border radius as needed
                    color: Colors.black.withOpacity(0.5), // Apply dark overlay directly to the background
                    image: DecorationImage(
                      image: AssetImage('lib/images/top-view-raw-potatoes-table.jpg'),
                      fit: BoxFit.cover,
                    ),
                  ),
                  padding: const EdgeInsets.all(5.0),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(
                        'Potatoes',
                        style: TextStyle(fontSize: 18, color: Colors.white, fontWeight: FontWeight.bold),
                      ),
                      Text(
                        '\€10.00', // Replace with the actual price
                        style: TextStyle(fontSize: 18, color: Colors.white, fontWeight: FontWeight.bold),
                      ),
                    ],
                  ),
                );
              },
            ),
          ),
        ],
      ),
            bottomNavigationBar: BottomAppBar(
        elevation: 0, // Remove the shadow
        child: SizedBox(
          height: 40, // Adjust the height as needed
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: [
              IconButton(
                icon: Icon(Icons.home),
                onPressed: () {
                  navigateToPage(context, '/');
                },
              ),
              IconButton(
                icon: Icon(Icons.shopping_cart),
                onPressed: () {
                  navigateToPage(context, '/cart');
                },
              ),
              IconButton(
                icon: Icon(Icons.add_box),
                onPressed: () {
                  navigateToPage(context, '/category');
                },
              ),
            ],
          ),
        ),
      ),
    );
  }
}



void navigateToPage(BuildContext context, String route) {
  Navigator.pushNamed(context, route);
}
