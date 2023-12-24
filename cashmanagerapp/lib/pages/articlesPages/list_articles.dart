import 'package:cashmanagerapp/models/articlemodel.dart';
import 'package:cashmanagerapp/pages/detailPages/detail.dart';
import 'package:cashmanagerapp/services/article_service.dart';
import 'package:flutter/material.dart';

class ListArticles extends StatefulWidget {
  ListArticles({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _ListArticlesState();
}


class _ListArticlesState extends State<ListArticles> {
  List<ArticleModel> articles = [];

  @override
  void initState() {
    super.initState();
    getAllArticles();
  }

  getAllArticles() async {
    final List<ArticleModel> articles =
        (await ArticleService().getAllArticles()).toList();
    setState(() {
      this.articles = articles;
    });
  }
  
  @override
  Widget build(BuildContext context){

    return Scaffold(
      appBar: AppBar(
        toolbarHeight: 40,
        title: Text('Articles',
            style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20)),
      ),
      //list of article only the imageUrl
      body: Column(
        children: [
          Expanded(
                child: ListView.builder(
                  itemCount: (articles.length / 2).ceil(),
                  itemBuilder: (context, index) {
                    int startIndex = index * 2;
                    int endIndex = startIndex + 2;
                    if (endIndex > articles.length) {
                      endIndex = articles.length;
                    }
                    List<ArticleModel> rowArticles = articles.sublist(startIndex, endIndex);
          
                    return Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Row(
                        children: rowArticles.map((article) {
                          return Expanded(
                            child: GestureDetector(
                              onTap: () {
                                    Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                    builder: (context) => Detail(
                                      idArticle: article.id.toString(),
                                    ),
                                  ),
                                );
                              },
                            child: Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Container(
                                width: 158.0,
                                height: 190.0,
                                decoration: BoxDecoration(
                                  borderRadius: BorderRadius.circular(20),
                                  image: DecorationImage(
                                    image: AssetImage(article.imageUrl),
                                    fit: BoxFit.cover,
                                  ),
                                ),
                              ),
                            ),
                            ),
                          );
                        }).toList(),
                      ),
                    );
                  },
                ),
              ),
        ],
      ),

    );
  }
}