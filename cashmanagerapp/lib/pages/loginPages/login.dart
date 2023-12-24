// ignore_for_file: use_build_context_synchronously

import 'package:cashmanagerapp/services/authentication_service.dart';
import 'package:flutter/material.dart';

const snackBar = SnackBar(
  content: Text('Wrong email or password'),
  backgroundColor: Colors.red,
  behavior: SnackBarBehavior.floating,
  duration: Duration(seconds: 3),
);


class Login extends StatefulWidget {
  @override
  State<Login> createState() => _LoginState();
}

class _LoginState extends State<Login>{
  TextEditingController emailController = TextEditingController();
  TextEditingController passwordController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(image:
          AssetImage('lib/images/login.jpg'),
          fit: BoxFit.cover,
          ),
        ),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.end,
          children: [
            Container(
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.only(
                  topLeft: Radius.circular(40.0),
                  topRight: Radius.circular(40.0)
                )
              ),
              padding: EdgeInsets.all(16.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    'SIGN IN',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 24.0,
                    ),
                  ),
                  SizedBox(height: 10.0),
                  TextField(
                    controller: emailController,
                    decoration: InputDecoration(
                      hintText: 'Email',
                      hintStyle: TextStyle(
                        color: Colors.black,
                        fontWeight: FontWeight.bold
                      ),
                      enabledBorder: OutlineInputBorder(
                        borderSide: BorderSide(color: Colors.yellow, width:3),
                        borderRadius: BorderRadius.circular(10.0),
                      ),
                    ),
                  ),
                  SizedBox(height: 10.0),
                  TextField(
                    obscureText: true,
                    controller: passwordController,
                    decoration: InputDecoration(
                      hintText: 'Password',
                      hintStyle: TextStyle(
                        color: Colors.black,
                        fontWeight: FontWeight.bold
                      ),
                      enabledBorder: OutlineInputBorder(
                        borderSide: BorderSide(color: Colors.yellow, width:3),
                        borderRadius: BorderRadius.circular(10.0),
                      ),
                    ),
                  ),
                  SizedBox(height: 10.0),
                  ElevatedButton(
                    onPressed: () async {
                      if (emailController.text.isEmpty || passwordController.text.isEmpty) {
                        ScaffoldMessenger.of(context).showSnackBar(snackBar);
                        return;
                      }
                      final bool emailValid = 
                      RegExp(r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+")
                        .hasMatch(emailController.text);
                      if (emailValid) {
                        // try {
                        // // await AuhtenticationService().login(emailController.text, passwordController.text);
                        // } catch (e) {
                        //   ScaffoldMessenger.of(context).showSnackBar(snackBar);
                        // }
                      }
                      // FOR DEVELOPMENT PURPOSES ONLY
                      await AuhtenticationService().login("user@example.com", "string");
                      Navigator.pushNamed(context, '/');
                      
                    },
                    style: ElevatedButton.styleFrom(
                      textStyle: TextStyle(
                        fontWeight: FontWeight.bold,
                      ),
                      backgroundColor: Colors.yellow,
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(10.0),
                      ),
                    ),
                    child: Padding(
                      padding: const EdgeInsets.only(right: 50.0, left: 50.0, top: 15.0, bottom: 15.0),
                      child: Text('Sign in',
                      style: TextStyle(
                        fontSize: 15.0,
                        color: Colors.black,
                      ),
                    ),
                    ),
                  ),
                  SizedBox(height: 10.0),
                  GestureDetector(
                    onTap: () {
                      Navigator.pushNamed(context, '/register');
                    },
                    child: Text(
                      'Create an account',
                      style: TextStyle(
                        color: Color.fromARGB(240, 242, 223, 52),
                        fontWeight: FontWeight.bold,
                      ),
                    ),),
                ],
              ),
            ),    
          ],
        ),
      ),
    );
  }
}