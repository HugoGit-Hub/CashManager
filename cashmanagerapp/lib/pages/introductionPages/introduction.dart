import 'package:cashmanagerapp/main.dart';
import 'package:flutter/material.dart';
import 'package:cashmanagerapp/widgets/app_large_text.dart';
import 'package:cashmanagerapp/widgets/app_text.dart';

class Introduction extends StatefulWidget {
  const Introduction({Key? key}) : super(key: key);

  @override
  State<Introduction> createState() => _IntroductionState();
}

class _IntroductionState extends State<Introduction> {
  List image = [
    'lib/images/intro1.png',
    'lib/images/intro2.png',
    'lib/images/intro3.png',
  ];
  List textLarge = ['', 'Bienvenue sur Cash Manager', 'A vos courses !'];
  List text = ['', 'L\'application qui accompagne vos courses', 'Scannez vos produits'];

  int currentPage = 0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: PageView.builder(
        scrollDirection: Axis.vertical,
        itemCount: 4, // Increase itemCount to 4
        onPageChanged: (index) {
          setState(() {
            currentPage = index;
          });
        },
        itemBuilder: (_, index) {
          if (index < 3) {
            return Container(
              width: double.maxFinite,
              height: double.maxFinite,
              decoration: BoxDecoration(
                color: Color(0xFFFFF500),
                image: DecorationImage(
                  image: AssetImage(image[index]),
                  fit: BoxFit.scaleDown,
                  alignment: Alignment.center,
                ),
              ),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.end,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Padding(
                    padding: EdgeInsets.only(),
                    child: AppLargeText(text: textLarge[index], color: Colors.black),
                  ),
                  Padding(
                    padding: EdgeInsets.only(bottom: 100),
                    child: AppText(text: text[index], color: Colors.black),
                  ),
                  SizedBox(height: 20), // Adjust as needed
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      Icon(
                        Icons.keyboard_arrow_down,
                        color: Colors.black,
                        size: 40.0,
                      ),
                    ],
                  ),
                ],
              ),
            );
          } else if (index == 3 && currentPage == 3) {
            // If it's the fourth page and manually reached, navigate to "/" route
            WidgetsBinding.instance.addPostFrameCallback((_) {
              Navigator.pushReplacementNamed(context,"/"); // Replace with your home route
            });
            return Container(); // Placeholder for the fourth page
          } else {
            return Container(); // Placeholder for other cases
          }
        },
      ),
    );
  }
}
