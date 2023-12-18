// ignore_for_file: use_build_context_synchronously

import 'package:cashmanagerapp/services/authenticationservice.dart';
import 'package:flutter/material.dart';

SnackBar snackBar = SnackBar(
  content: Text('An error occured'),
  backgroundColor: Colors.red,
  behavior: SnackBarBehavior.floating,
  duration: Duration(seconds: 3),
);


class Register extends StatefulWidget {
  @override
  State<Register> createState() => _RegisterState();
}

class _RegisterState extends State<Register>{
  TextEditingController firstnameController = TextEditingController();
  TextEditingController lastnameController = TextEditingController();
  TextEditingController emailController = TextEditingController();
  TextEditingController passwordController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(image:
          AssetImage('lib/images/register.jpg'),
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
                    'Create your account',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 24.0,
                    ),
                  ),
                  SizedBox(height: 10.0),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Expanded(
                        child: TextField(
                          controller: firstnameController,
                          decoration: InputDecoration(
                            hintText: 'First Name',
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
                      ),
                      SizedBox(width: 10.0),
                      Expanded(
                        child: TextField(
                          controller: lastnameController,
                          decoration: InputDecoration(
                            hintText: 'Last Name',
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
                      ),
                    ],
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
                    try {
                      String token = await AuhtenticationService().register(firstnameController.text,lastnameController.text,emailController.text,passwordController.text);
                      print(token);
                      Navigator.pushNamed(context, '/welcome');
                    } catch (e) {
                      ScaffoldMessenger.of(context).showSnackBar(snackBar);
                    }
                    
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
                      child: Text('Sign up',
                      style: TextStyle(
                        fontSize: 15.0,
                        color: const Color.fromRGBO(0, 0, 0, 1),
                      ),
                    ),
                    ),
                  ),
                  SizedBox(height: 10.0),
                  GestureDetector(
                    onTap: (){
                      Navigator.pushNamed(context, '/login');             
                    },
                    child: Text(
                      'Already have an account? Sign in',
                      style: TextStyle(
                        color: Colors.yellow,
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