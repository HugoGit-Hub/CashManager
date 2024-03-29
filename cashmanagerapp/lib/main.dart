import 'package:cashmanagerapp/pages/articlesPages/list_articles.dart';
import 'package:cashmanagerapp/pages/cartPages/cart.dart';
import 'package:cashmanagerapp/pages/categoryPages/category_dairy.dart';
import 'package:cashmanagerapp/pages/categoryPages/category_fruit.dart';
import 'package:cashmanagerapp/pages/categoryPages/category_vegetable.dart';
import 'package:cashmanagerapp/pages/checkoutPages/banking_checkout.dart';
import 'package:cashmanagerapp/pages/checkoutPages/paiement.dart';
import 'package:cashmanagerapp/pages/introductionPages/introduction.dart';
import 'package:cashmanagerapp/pages/loginPages/login.dart';
import 'package:cashmanagerapp/pages/loginPages/register.dart';
import 'package:cashmanagerapp/services/article_service.dart';
import 'package:flutter/material.dart';
import 'pages/scanPages/scanhome.dart';
import 'pages/categoryPages/category.dart';
import 'pages/detailPages/detail.dart';
import 'package:cashmanagerapp/models/cartitem_model.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatefulWidget {
  @override
  State<MyApp> createState() => _MyApp();
}

class _MyApp extends State<MyApp> {
  final ScanHome scanHome = ScanHome();
  final Category category = Category();
  final CategoryDairy categoryDairy = CategoryDairy();
  final CategoryVegetable categoryVegetable = CategoryVegetable();
  final CategoryFruit categoryFruit = CategoryFruit();
  final Cart cart = Cart();
  final Detail detail = Detail(idArticle: "0", quantity: 1);
  final Introduction introduction = Introduction();
  final Login login = Login();
  final Register register = Register();
  final Paiement paiement = Paiement();
  final ListArticles listArticles = ListArticles();

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'CashManager',
      theme: ThemeData(
        useMaterial3: true,
        colorScheme:
            ColorScheme.fromSeed(seedColor: Color.fromARGB(255, 255, 231, 19)),
      ),
      initialRoute: '/login',
      routes: {
        '/': (context) => HomePage(),
        '/scanHome': (context) => scanHome,
        '/category': (context) => category,
        '/category/fruit': (context) => categoryFruit,
        '/category/vegetable': (context) => categoryVegetable,
        '/category/dairy': (context) => categoryDairy,
        '/cart': (context) => cart,
        '/login': (context) => login,
        '/detail': (context) => detail,
        '/register': (context) => register,
        '/welcome': (context) => introduction,
        '/paiement': (context) => paiement,
        '/listArticles': (context) => listArticles,
        '/paiement/confirmation': (context) => BankingCheckout(),
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
        page = ListArticles();
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
              label: 'Articles',
            ),
          ],
        ),
      );
    });
  }
}

class Accueil extends StatefulWidget {
  @override
  State<Accueil> createState() => _Accueil();
}

class _Accueil extends State<Accueil>  with WidgetsBindingObserver {
  List<CartItemModel> cartitems = [];
  @override
  void initState() {
    super.initState();
    getAllCartItem();
  }

  @override
  void dispose() {
    WidgetsBinding.instance.removeObserver(this);
    super.dispose();
  }

  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    if (state == AppLifecycleState.resumed) {
      getAllCartItem();
    }
  }


  getAllCartItem() async {
    final List<CartItemModel> cartitems =
        (await ArticleService().getAllCartItem()).toList();
    setState(() {
      this.cartitems = cartitems;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // appBar: AppBar(
      //   toolbarHeight: 40,
      //   title: Text('Accueil',
      //       style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20)),
      // ),
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          SizedBox(height: 50), // Add spacing
          // Row 1
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: Text('Bienvenue !',
                    style:
                        TextStyle(fontWeight: FontWeight.bold, fontSize: 30)),
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
            height: 130, // Adjust the height as needed
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
                          size: 100, color: Colors.black), // Add barcode icon
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
                child: Text('Catégories',
                    style:
                        TextStyle(fontWeight: FontWeight.bold, fontSize: 25)),
              ),
              TextButton(
                onPressed: () {
                  navigateToPage(context, '/category');
                },
                child: Icon(Icons.arrow_forward_ios),
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
                style: ElevatedButton.styleFrom(
                    padding: EdgeInsets.symmetric(horizontal: 30, vertical: 20),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12))),

                child: ImageIcon(
                    AssetImage("lib/images/fruit.png"),
                    color: const Color.fromARGB(230, 127, 30, 1),
                    size: 50,
                ),
              ),
              ElevatedButton(
                onPressed: () { 
                  navigateToPage(context, '/category/vegetable');
                },
                style: ElevatedButton.styleFrom(
                    padding: EdgeInsets.symmetric(horizontal: 30, vertical: 20),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12))),
                 child: ImageIcon(
                    AssetImage("lib/images/vegetable.png"),
                    color: const Color.fromARGB(230, 127, 30, 1),
                    size: 50,
                ),
              ),
              ElevatedButton(
                onPressed: () {
                  navigateToPage(context, '/category/dairy');
                },
                style: ElevatedButton.styleFrom(
                    padding: EdgeInsets.symmetric(horizontal: 30, vertical: 20),
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12))),
                child: ImageIcon(
                    AssetImage("lib/images/dairy.png"),
                    color: const Color.fromARGB(230, 127, 30, 1),
                    size: 50,
                ),
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
                child: Text('Panier',
                    style:
                        TextStyle(fontWeight: FontWeight.bold, fontSize: 25)),
              ),
            ],
          ),
          // Row 6 (Placeholder for the list of items)
          Expanded(
            child: ListView.builder(
              itemCount: cartitems.length,
              itemBuilder: (context, index) {
                var cartitem = cartitems[index];
                return GestureDetector(
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => Detail(
                            idArticle: cartitem.articleId.toString(),
                            quantity: cartitem.quantity,
                          ),
                        ),
                      );
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
                          image: AssetImage(cartitem.imageUrl),
                          fit: BoxFit.cover,
                        ),
                      ),
                      padding: const EdgeInsets.all(5.0),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          Text(
                            cartitem.articleName,
                            style: TextStyle(
                                fontSize: 18,
                                color: Colors.white,
                                fontWeight: FontWeight.bold),
                          ),
                          Text(
                            cartitem.totalArticlePrice.toString(),
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
