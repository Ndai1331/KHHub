import 'package:flutter/material.dart';

class PlaceDetailArgs {
  const PlaceDetailArgs({
    required this.title,
    required this.subtitle,
    required this.coverUrl,
    required this.thumbnailUrl,
    required this.description,
    required this.galleryUrls,
    required this.initialRating,
    required this.initialFavoriteCount,
    required this.initialComments,
  });

  final String title;
  final String subtitle;
  final String coverUrl;
  final String thumbnailUrl;
  final String description;
  final List<String> galleryUrls;
  final double initialRating;
  final int initialFavoriteCount;
  final List<String> initialComments;
}

class PlaceDetailPage extends StatefulWidget {
  const PlaceDetailPage({super.key});

  static const String routeName = '/place-detail';

  @override
  State<PlaceDetailPage> createState() => _PlaceDetailPageState();
}

class _PlaceDetailPageState extends State<PlaceDetailPage> {
  final TextEditingController _commentController = TextEditingController();
  late List<String> _comments;
  late double _rating;
  late int _favoriteCount;
  bool _isFavorite = false;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    final args = ModalRoute.of(context)!.settings.arguments! as PlaceDetailArgs;
    _comments = List<String>.from(args.initialComments);
    _rating = args.initialRating;
    _favoriteCount = args.initialFavoriteCount;
  }

  @override
  void dispose() {
    _commentController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final args = ModalRoute.of(context)!.settings.arguments! as PlaceDetailArgs;
    return Scaffold(
      appBar: AppBar(title: const Text('Chi tiết địa điểm')),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: <Widget>[
          ClipRRect(
            borderRadius: BorderRadius.circular(16),
            child: Image.network(args.coverUrl, height: 220, fit: BoxFit.cover),
          ),
          const SizedBox(height: 14),
          Row(
            children: <Widget>[
              ClipRRect(
                borderRadius: BorderRadius.circular(10),
                child: Image.network(args.thumbnailUrl, width: 64, height: 64, fit: BoxFit.cover),
              ),
              const SizedBox(width: 10),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Text(args.title, style: const TextStyle(fontSize: 18, fontWeight: FontWeight.w700)),
                    const SizedBox(height: 4),
                    Text(args.subtitle, style: const TextStyle(fontSize: 13, color: Color(0xFF546E7A))),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(height: 14),
          Text(args.description, style: const TextStyle(height: 1.45)),
          const SizedBox(height: 16),
          const Text('Gallery', style: TextStyle(fontSize: 16, fontWeight: FontWeight.w700)),
          const SizedBox(height: 8),
          SizedBox(
            height: 96,
            child: ListView.separated(
              scrollDirection: Axis.horizontal,
              itemBuilder: (BuildContext context, int index) => GestureDetector(
                onTap: () => _openGalleryViewer(
                  context: context,
                  images: args.galleryUrls,
                  initialIndex: index,
                ),
                child: ClipRRect(
                  borderRadius: BorderRadius.circular(12),
                  child: Image.network(args.galleryUrls[index], width: 140, fit: BoxFit.cover),
                ),
              ),
              separatorBuilder: (BuildContext context, int index) => const SizedBox(width: 8),
              itemCount: args.galleryUrls.length,
            ),
          ),
          const SizedBox(height: 16),
          _SocialBar(
            isFavorite: _isFavorite,
            favoriteCount: _favoriteCount,
            rating: _rating,
            onFavorite: () {
              setState(() {
                _isFavorite = !_isFavorite;
                _favoriteCount += _isFavorite ? 1 : -1;
              });
            },
            onRate: (double value) => setState(() => _rating = value),
          ),
          const SizedBox(height: 18),
          const Text('Bình luận', style: TextStyle(fontSize: 16, fontWeight: FontWeight.w700)),
          const SizedBox(height: 8),
          ..._comments.map(
            (String comment) => Container(
              margin: const EdgeInsets.only(bottom: 8),
              padding: const EdgeInsets.all(12),
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.circular(10),
                boxShadow: <BoxShadow>[
                  BoxShadow(
                    color: Colors.black.withValues(alpha: 0.04),
                    blurRadius: 8,
                    offset: const Offset(0, 3),
                  ),
                ],
              ),
              child: Text(comment),
            ),
          ),
          const SizedBox(height: 10),
          TextField(
            controller: _commentController,
            decoration: InputDecoration(
              hintText: 'Viết bình luận...',
              suffixIcon: IconButton(
                onPressed: () {
                  final value = _commentController.text.trim();
                  if (value.isEmpty) return;
                  setState(() => _comments.insert(0, value));
                  _commentController.clear();
                },
                icon: const Icon(Icons.send_rounded),
              ),
            ),
          ),
        ],
      ),
    );
  }

  void _openGalleryViewer({
    required BuildContext context,
    required List<String> images,
    required int initialIndex,
  }) {
    showDialog<void>(
      context: context,
      barrierColor: Colors.black.withValues(alpha: 0.92),
      builder: (BuildContext context) => _GalleryViewerDialog(
        images: images,
        initialIndex: initialIndex,
      ),
    );
  }
}

class _SocialBar extends StatelessWidget {
  const _SocialBar({
    required this.isFavorite,
    required this.favoriteCount,
    required this.rating,
    required this.onFavorite,
    required this.onRate,
  });

  final bool isFavorite;
  final int favoriteCount;
  final double rating;
  final VoidCallback onFavorite;
  final ValueChanged<double> onRate;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: <Widget>[
        IconButton(
          onPressed: onFavorite,
          icon: Icon(
            isFavorite ? Icons.favorite_rounded : Icons.favorite_border_rounded,
            color: isFavorite ? Colors.redAccent : const Color(0xFF78909C),
          ),
        ),
        Text('$favoriteCount'),
        const SizedBox(width: 16),
        const Icon(Icons.star_rounded, color: Color(0xFFFFB300)),
        const SizedBox(width: 4),
        Text(rating.toStringAsFixed(1)),
        const Spacer(),
        PopupMenuButton<double>(
          onSelected: onRate,
          itemBuilder: (BuildContext context) => List<PopupMenuEntry<double>>.generate(
            5,
            (int index) {
              final value = index + 1.0;
              return PopupMenuItem<double>(
                value: value,
                child: Text('Đánh giá $value sao'),
              );
            },
          ),
          child: const Padding(
            padding: EdgeInsets.symmetric(horizontal: 10, vertical: 8),
            child: Row(
              children: <Widget>[
                Icon(Icons.rate_review_rounded, size: 18),
                SizedBox(width: 4),
                Text('Đánh giá'),
              ],
            ),
          ),
        ),
      ],
    );
  }
}

class _GalleryViewerDialog extends StatefulWidget {
  const _GalleryViewerDialog({
    required this.images,
    required this.initialIndex,
  });

  final List<String> images;
  final int initialIndex;

  @override
  State<_GalleryViewerDialog> createState() => _GalleryViewerDialogState();
}

class _GalleryViewerDialogState extends State<_GalleryViewerDialog> {
  late final PageController _pageController = PageController(initialPage: widget.initialIndex);
  late int _currentIndex = widget.initialIndex;

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  void _goTo(int index) {
    _pageController.animateToPage(
      index,
      duration: const Duration(milliseconds: 220),
      curve: Curves.easeOut,
    );
  }

  @override
  Widget build(BuildContext context) {
    final hasPrev = _currentIndex > 0;
    final hasNext = _currentIndex < widget.images.length - 1;

    return Dialog(
      insetPadding: const EdgeInsets.all(12),
      backgroundColor: Colors.transparent,
      child: Stack(
        children: <Widget>[
          ClipRRect(
            borderRadius: BorderRadius.circular(16),
            child: Container(
              color: Colors.black,
              child: PageView.builder(
                controller: _pageController,
                itemCount: widget.images.length,
                onPageChanged: (int index) => setState(() => _currentIndex = index),
                itemBuilder: (BuildContext context, int index) {
                  return InteractiveViewer(
                    child: Image.network(
                      widget.images[index],
                      fit: BoxFit.contain,
                    ),
                  );
                },
              ),
            ),
          ),
          Positioned(
            top: 10,
            right: 10,
            child: IconButton(
              onPressed: () => Navigator.pop(context),
              icon: const Icon(Icons.close_rounded, color: Colors.white),
            ),
          ),
          Positioned(
            left: 10,
            top: 0,
            bottom: 0,
            child: Center(
              child: IconButton(
                onPressed: hasPrev ? () => _goTo(_currentIndex - 1) : null,
                icon: Icon(
                  Icons.arrow_back_ios_new_rounded,
                  color: hasPrev ? Colors.white : Colors.white38,
                ),
              ),
            ),
          ),
          Positioned(
            right: 10,
            top: 0,
            bottom: 0,
            child: Center(
              child: IconButton(
                onPressed: hasNext ? () => _goTo(_currentIndex + 1) : null,
                icon: Icon(
                  Icons.arrow_forward_ios_rounded,
                  color: hasNext ? Colors.white : Colors.white38,
                ),
              ),
            ),
          ),
          Positioned(
            bottom: 12,
            left: 0,
            right: 0,
            child: Center(
              child: Container(
                padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 6),
                decoration: BoxDecoration(
                  color: Colors.black54,
                  borderRadius: BorderRadius.circular(16),
                ),
                child: Text(
                  '${_currentIndex + 1}/${widget.images.length}',
                  style: const TextStyle(color: Colors.white, fontWeight: FontWeight.w600),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
