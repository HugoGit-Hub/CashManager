import 'package:cashmanagerapp/pages/cartPages/cart.dart';
import 'package:cashmanagerapp/pages/categoryPages/category_dairy.dart';
import 'package:cashmanagerapp/pages/categoryPages/category_fruit.dart';
import 'package:cashmanagerapp/pages/categoryPages/category_vegetable.dart';
import 'package:cashmanagerapp/pages/introductionPages/introduction.dart';
import 'package:flutter/material.dart';
import 'pages/scanPages/scan_home.dart';
import 'pages/categoryPages/category.dart';
import 'pages/detailPages/detail.dart';

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
  final Detail detail = Detail();
  final Introduction introduction = Introduction();

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'CashManager',
      theme: ThemeData(
        useMaterial3: true,
        colorScheme:
            ColorScheme.fromSeed(seedColor: Color.fromARGB(255, 255, 231, 19)),
      ),
      initialRoute: '/',
      routes: {
        '/': (context) => HomePage(),
        '/scanHome': (context) => scanHome,
        '/category': (context) => category,
        '/category/fruit': (context) => categoryFruit,
        '/category/vegetable': (context) => categoryVegetable,
        '/category/dairy': (context) => categoryDairy,
        '/cart': (context) => cart,
        '/welcome' : (context) => introduction,
        '/detail': (context) => detail,
      },
    );
  }
}

class HomePage extends StatefulWidget {
  @override
  State<HomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<HomePage> {
  var pos = 0;
  @override
  Widget build(BuildContext context) {
    Widget page = Accueil();

    switch (pos) {
      case 0:
        page = Accueil();
        break;
      case 1:
        page = Cart();
        break;
      case 2:
        page = Category();
        break;
      case 3:
        page = Detail();
        break;
      // case 3:
      //   page = UserPage();
      //   break;
    }
    return LayoutBuilder(builder: (context, constraints) {
      return Scaffold(
        body: page,
        bottomNavigationBar: BottomNavigationBar(
          currentIndex: pos,
          onTap: (value) {
            setState(() {
              pos = value;
            });
          },
          items: [
            BottomNavigationBarItem(
              icon: Icon(Icons.home),
              label: 'Accueil',
            ),
            BottomNavigationBarItem(
              icon: Icon(Icons.shopping_cart),
              label: 'Panier',
            ),
            BottomNavigationBarItem(
              icon: Icon(Icons.category),
              label: 'Catégories',
            ),
          ],
        ),
      );
    });
  }
}

class Accueil extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        toolbarHeight: 40,
        title: Text('Accueil',
            style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20)),
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
                child: Text('Bienvenue',
                    style:
                        TextStyle(fontWeight: FontWeight.bold, fontSize: 18)),
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
                      borderRadius: BorderRadius.circular(12))),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Row(
                    children: [
                      Icon(Icons.qr_code_2_sharp,
                          size: 50, color: Colors.black), // Add barcode icon
                      SizedBox(width: 8), // Add spacing between icon and text
                      Text('Mode scanner',
                          style: TextStyle(fontSize: 24, color: Colors.black)),
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
                child: Text('Categories',
                    style:
                        TextStyle(fontWeight: FontWeight.bold, fontSize: 18)),
              ),
              TextButton(
                onPressed: () {
                  navigateToPage(context, '/category');
                },
                child: Text('Plus'),
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
                child: Text('Fruits'),
              ),
              ElevatedButton(
                onPressed: () {
                  navigateToPage(context, '/category/vegetable');
                },
                child: Text('Légumes'),
              ),
              ElevatedButton(
                onPressed: () {
                  navigateToPage(context, '/category/dairy');
                },
                child: Text('Laitiers'),
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
                child: Text('Pannier',
                    style:
                        TextStyle(fontWeight: FontWeight.bold, fontSize: 18)),
              ),
              TextButton(
                onPressed: () {
                  navigateToPage(context, '/cart');
                },
                child: Text('Plus'),
              ),
            ],
          ),
          SizedBox(height: 10), // Add spacing
          // Row 6 (Placeholder for the list of items)
          Expanded(
            child: ListView.builder(
              itemCount: 5,
              itemBuilder: (context, index) {
                return GestureDetector(
                    onTap: () {
                      navigateToPage(context, '/detail');
                    },
                    child: Container(
                      margin: EdgeInsets.symmetric(
                          vertical: 8,
                          horizontal: 5), // Add space between items
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(
                            20), // Adjust the border radius as needed
                        color: Colors.black.withOpacity(
                            0.5), // Apply dark overlay directly to the background
                        image: DecorationImage(
                          image: AssetImage(
                              'lib/images/top-view-raw-potatoes-table.jpg'),
                          fit: BoxFit.cover,
                        ),
                      ),
                      padding: const EdgeInsets.all(5.0),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          Text(
                            'Patates',
                            style: TextStyle(
                                fontSize: 18,
                                color: Colors.white,
                                fontWeight: FontWeight.bold),
                          ),
                          Text(
                            '10.00€', // Replace with the actual price
                            style: TextStyle(
                                fontSize: 18,
                                color: Colors.white,
                                fontWeight: FontWeight.bold),
                          ),
                        ],
                      ),
                    ));
              },
            ),
          )
        ],
      ),
    );
  }

  void navigateToPage(BuildContext context, String route) {
    Navigator.pushNamed(context, route);
  }
}
